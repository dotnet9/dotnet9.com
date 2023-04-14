﻿global using Dapr.Actors;
global using Dapr.Actors.Runtime;
global using Dotnet9.Commons;
global using Dotnet9.Contracts.Dto;
global using Dotnet9.Contracts.IntegrationEvents;
global using Dotnet9.Contracts.Requests;
global using Dotnet9.Service.Actors;
global using Dotnet9.Service.Application.FriendlyLinks.Commands;
global using Dotnet9.Service.Application.FriendlyLinks.Queries;
global using Dotnet9.Service.Domain.Aggregates.Abouts;
global using Dotnet9.Service.Domain.Aggregates.FriendlyLinks;
global using Dotnet9.Service.Domain.Repositories;
global using Dotnet9.Service.Infrastructure;
global using Dotnet9.Service.Infrastructure.Middleware;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Masa.BuildingBlocks.Caching;
global using Masa.BuildingBlocks.Data;
global using Masa.BuildingBlocks.Data.UoW;
global using Masa.BuildingBlocks.Ddd.Domain;
global using Masa.BuildingBlocks.Ddd.Domain.Entities.Full;
global using Masa.BuildingBlocks.Ddd.Domain.Events;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.BuildingBlocks.Ddd.Domain.Services;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
global using Masa.Contrib.Ddd.Domain.Repository.EFCore;
global using Masa.Contrib.Dispatcher.Events;
global using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
global using Masa.Utils.Models;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.OpenApi.Models;
global using System.Linq.Expressions;
global using System.Reflection;
global using Dotnet9.Service.Domain.Aggregates.ActionLogs;
global using Dotnet9.Service.Domain.Aggregates.Categories;
global using Dotnet9.Service.Domain.Aggregates.Albums;
global using Dotnet9.Service.Domain.Aggregates.Comments;
global using Dotnet9.Service.Domain.Aggregates.Donations;
global using Dotnet9.Service.Domain.Aggregates.Privacies;
global using Dotnet9.Service.Domain.Aggregates.Tags;
global using Dotnet9.Service.Domain.Aggregates.Timelines;
global using System.Runtime.Serialization;
global using Masa.BuildingBlocks.Ddd.Domain.Entities;
global using Dotnet9.Service.Domain.Aggregates.Blogs;
global using Microsoft.Extensions.Options;
global using System.Diagnostics;
global using System.Text.Json;
global using Dotnet9.Service.Services;
global using Dotnet9.Service.Domain.Aggregates;
global using Dotnet9.Service.Infrastructure.Repositories;
global using Dotnet9.Contracts.Dto.Categories;
global using Dotnet9.Service.Application.Categories.Queries;
global using Dotnet9.Contracts.Dto.FriendlyLinks;
global using Dotnet9.Contracts.Dto.Albums;
global using Dotnet9.Service.Application.Albums.Queries;
global using Dotnet9.Contracts.Dto.Blogs;
global using Dotnet9.Service.Application.Blogs.Queries;
global using Dotnet9.Contracts.Tags;
global using Dotnet9.Service.Application.Tags.Queries;