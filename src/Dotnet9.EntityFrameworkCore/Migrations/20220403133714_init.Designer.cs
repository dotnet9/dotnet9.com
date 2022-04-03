﻿// <auto-generated />
using System;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dotnet9.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(Dotnet9DbContext))]
    [Migration("20220403133714_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.2.22153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dotnet9.Domain.Abouts.About", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreateDate");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int")
                        .HasColumnName("CreateUserId");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeleteDate");

                    b.Property<int?>("DeleteUserId")
                        .HasColumnType("int")
                        .HasColumnName("DeleteUserId");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdateDate");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int")
                        .HasColumnName("UpdateUserId");

                    b.HasKey("Id");

                    b.ToTable("AppAbouts", (string)null);
                });

            modelBuilder.Entity("Dotnet9.Domain.Albums.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Cover")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreateDate");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int")
                        .HasColumnName("CreateUserId");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeleteDate");

                    b.Property<int?>("DeleteUserId")
                        .HasColumnType("int")
                        .HasColumnName("DeleteUserId");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdateDate");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int")
                        .HasColumnName("UpdateUserId");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("Slug");

                    b.ToTable("AppAlbums", (string)null);
                });

            modelBuilder.Entity("Dotnet9.Domain.Blogs.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("BriefDescription")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.Property<int>("CopyrightType")
                        .HasColumnType("int");

                    b.Property<string>("Cover")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreateDate");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int")
                        .HasColumnName("CreateUserId");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeleteDate");

                    b.Property<int?>("DeleteUserId")
                        .HasColumnType("int")
                        .HasColumnName("DeleteUserId");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Original")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("OriginalAvatar")
                        .HasColumnType("longtext");

                    b.Property<string>("OriginalLink")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("OriginalTitle")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdateDate");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int")
                        .HasColumnName("UpdateUserId");

                    b.HasKey("Id");

                    b.HasIndex("Slug");

                    b.HasIndex("Title");

                    b.ToTable("AppBlogPosts", (string)null);
                });

            modelBuilder.Entity("Dotnet9.Domain.Blogs.BlogPostAlbum", b =>
                {
                    b.Property<int>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.HasKey("BlogPostId", "AlbumId");

                    b.HasIndex("AlbumId");

                    b.HasIndex("BlogPostId", "AlbumId");

                    b.ToTable("AppBlogPostAlbums", (string)null);
                });

            modelBuilder.Entity("Dotnet9.Domain.Blogs.BlogPostCategory", b =>
                {
                    b.Property<int>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("BlogPostId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("BlogPostId", "CategoryId");

                    b.ToTable("AppBlogPostCategories", (string)null);
                });

            modelBuilder.Entity("Dotnet9.Domain.Blogs.BlogPostTag", b =>
                {
                    b.Property<int>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("BlogPostId", "TagId");

                    b.HasIndex("TagId");

                    b.HasIndex("BlogPostId", "TagId");

                    b.ToTable("AppBlogPostTags", (string)null);
                });

            modelBuilder.Entity("Dotnet9.Domain.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Cover")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreateDate");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int")
                        .HasColumnName("CreateUserId");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeleteDate");

                    b.Property<int?>("DeleteUserId")
                        .HasColumnType("int")
                        .HasColumnName("DeleteUserId");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdateDate");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int")
                        .HasColumnName("UpdateUserId");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("Slug");

                    b.ToTable("AppCategories", (string)null);
                });

            modelBuilder.Entity("Dotnet9.Domain.Tags.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreateDate");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int")
                        .HasColumnName("CreateUserId");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeleteDate");

                    b.Property<int?>("DeleteUserId")
                        .HasColumnType("int")
                        .HasColumnName("DeleteUserId");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdateDate");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int")
                        .HasColumnName("UpdateUserId");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("AppTags", (string)null);
                });

            modelBuilder.Entity("Dotnet9.Domain.UrlLinks.UrlLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreateDate");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int")
                        .HasColumnName("CreateUserId");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("DeleteDate");

                    b.Property<int?>("DeleteUserId")
                        .HasColumnType("int")
                        .HasColumnName("DeleteUserId");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsDeleted");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdateDate");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int")
                        .HasColumnName("UpdateUserId");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("Url");

                    b.ToTable("AppUrlLinks", (string)null);
                });

            modelBuilder.Entity("Dotnet9.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Account")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CreateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DeleteUserId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UpdateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Dotnet9.Domain.Blogs.BlogPostAlbum", b =>
                {
                    b.HasOne("Dotnet9.Domain.Albums.Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dotnet9.Domain.Blogs.BlogPost", null)
                        .WithMany("Albums")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dotnet9.Domain.Blogs.BlogPostCategory", b =>
                {
                    b.HasOne("Dotnet9.Domain.Blogs.BlogPost", null)
                        .WithMany("Categories")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dotnet9.Domain.Categories.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dotnet9.Domain.Blogs.BlogPostTag", b =>
                {
                    b.HasOne("Dotnet9.Domain.Blogs.BlogPost", null)
                        .WithMany("Tags")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dotnet9.Domain.Tags.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dotnet9.Domain.Blogs.BlogPost", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("Categories");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
