﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Tera.Game;

namespace DamageMeter
{
    public class AbnormalityDuration: ICloneable
    {
        public long Duration()
        {
            var end = DateTime.Now.Ticks/TimeSpan.TicksPerSecond;

            long totalDuration = 0;

            foreach (var duration in ListDuration)
            {
                if (end < duration.Begin)
                {
                    continue;
                }

                var abnormalityBegin = duration.Begin;
                var abnormalityEnd = duration.End;
               
                if (end < abnormalityEnd)
                {
                    abnormalityEnd = end;
                }

                totalDuration += abnormalityEnd - abnormalityBegin;
            }
            return totalDuration;
        }

        public long Duration(long begin, long end)
        {
            long totalDuration = 0;
            
            foreach (var duration in ListDuration)
            {

                if (begin > duration.End || end < duration.Begin)
                {
                    continue;
                }

                var abnormalityBegin = duration.Begin;
                var abnormalityEnd = duration.End;
                if (begin > abnormalityBegin)
                {
                    abnormalityBegin = begin;
                }

                if (end < abnormalityEnd)
                {
                    abnormalityEnd = end;
                }

                totalDuration += abnormalityEnd - abnormalityBegin;


            }
            return totalDuration;
        }

        public AbnormalityDuration(PlayerClass playerClass)
        {
            InitialPlayerClass = playerClass;
        }

        public PlayerClass InitialPlayerClass { get; }


        public bool Ended()
        {
            return ListDuration[ListDuration.Count - 1].End != long.MaxValue;
        }

        public List<Duration> ListDuration = new List<Duration>(); 

        public object Clone()
        {

            var newListDuration = ListDuration.Select(duration => (Duration) duration.Clone()).ToList();
            var abnormalityDuration = new AbnormalityDuration(InitialPlayerClass)
            {
                ListDuration =  newListDuration
            };
            return abnormalityDuration;
        }
    }
}