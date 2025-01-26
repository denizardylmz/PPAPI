﻿using AutoMapper;
using DC = HomeProjectAPI.API.DataContracts;
using S = HomeProjectAPI.Services.Model;
using Sql = Services.Sql;

namespace HomeProjectAPI.IoC.Configuration.AutoMapper.Profiles
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            CreateMap<DC.User, S.User>().ReverseMap();
            CreateMap<DC.UserCreation, S.UserCreation>().ReverseMap();
            CreateMap<DC.Address, S.Address>().ReverseMap();

            CreateMap<
                DC.Requests.Request<DC.UserCreation>,
                S.Requests.Request<S.UserCreation>
            >().ReverseMap();

            CreateMap<
                DC.Responses.Response<DC.Requests.Request<DC.UserCreation>, DC.User>,
                S.Responses.Response<S.Requests.Request<S.UserCreation>, S.User>
            >().ReverseMap();

            CreateMap<
                DC.Requests.Request<DC.User>,
                S.Requests.Request<S.User>
            >().ReverseMap();

            CreateMap<
                DC.Responses.Response<DC.Requests.Request<DC.User>, DC.User>,
                S.Responses.Response<S.Requests.Request<S.User>, S.User>
            >().ReverseMap();

            CreateMap<
                DC.Requests.Request<string>,
                S.Requests.Request<string>
            >().ReverseMap();

            CreateMap<
                DC.Responses.Response<DC.Requests.Request<string>, int>,
                S.Responses.Response<S.Requests.Request<string>, int>
            >().ReverseMap();
            
            #region Notes
            CreateMap<Sql.Models.NoteTag, S.NoteTag>();
            CreateMap<Sql.Models.Tag, S.Tag>();
            CreateMap<Sql.Models.Note, S.Note>();
            #endregion
        }
    }
}