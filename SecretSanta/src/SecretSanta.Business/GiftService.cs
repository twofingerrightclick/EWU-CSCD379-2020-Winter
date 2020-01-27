using AutoMapper;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SecretSanta.Business
{
    class GiftService
    {
        static MapperConfiguration InitializeGiftNoIdMapper()
        {
            var config = new MapperConfiguration(cfg => {
               
                cfg.CreateMap<Gift,Gift >().Ignore(gift=>gift.Id);
            });
            return config;
        }

        MapperConfiguration _MapperConfiguration = InitializeGiftNoIdMapper();





    }
}
