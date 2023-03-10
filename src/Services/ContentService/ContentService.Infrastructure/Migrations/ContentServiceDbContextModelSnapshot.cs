﻿// <auto-generated />
using System;
using ContentService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContentService.Infrastructure.Migrations
{
    [DbContext(typeof(ContentServiceDbContext))]
    partial class ContentServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Content")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContentService.Core.AggregateModel.ContentAggregate.Content", b =>
                {
                    b.Property<Guid>("ContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("JsonSchemaModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContentId");

                    b.HasIndex("JsonSchemaModelId");

                    b.ToTable("Contents", "Content");
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.FieldConfigAggregate.FieldConfig", b =>
                {
                    b.Property<Guid>("FieldConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FormConfigId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FieldConfigId");

                    b.HasIndex("FormConfigId");

                    b.ToTable("FieldConfigs", "Content");
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.FormConfigAggregate.FormConfig", b =>
                {
                    b.Property<Guid>("FormConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FormConfigId");

                    b.ToTable("FormConfigs", "Content");
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.JsonPropertyModelAggregate.JsonPropertyModel", b =>
                {
                    b.Property<Guid>("JsonPropertyModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("JsonSchemaModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JsonPropertyModelId");

                    b.HasIndex("JsonSchemaModelId");

                    b.ToTable("JsonPropertyModels", "Content");
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.JsonSchemaModelAggregate.JsonSchemaModel", b =>
                {
                    b.Property<Guid>("JsonSchemaModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FormConfigId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("JsonSchemaModelId");

                    b.HasIndex("FormConfigId");

                    b.ToTable("JsonSchemaModels", "Content");
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.UserAggregate.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users", "Content");
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.ContentAggregate.Content", b =>
                {
                    b.HasOne("ContentService.Core.AggregateModel.JsonSchemaModelAggregate.JsonSchemaModel", null)
                        .WithMany("Content")
                        .HasForeignKey("JsonSchemaModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.FieldConfigAggregate.FieldConfig", b =>
                {
                    b.HasOne("ContentService.Core.AggregateModel.FormConfigAggregate.FormConfig", null)
                        .WithMany("Fields")
                        .HasForeignKey("FormConfigId");

                    b.OwnsOne("ContentService.Core.AggregateModel.FieldConfigAggregate.Props", "Props", b1 =>
                        {
                            b1.Property<Guid>("FieldConfigId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Label")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Placeholder")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("Required")
                                .HasColumnType("bit");

                            b1.HasKey("FieldConfigId");

                            b1.ToTable("FieldConfigs", "Content");

                            b1.WithOwner()
                                .HasForeignKey("FieldConfigId");
                        });

                    b.Navigation("Props")
                        .IsRequired();
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.JsonPropertyModelAggregate.JsonPropertyModel", b =>
                {
                    b.HasOne("ContentService.Core.AggregateModel.JsonSchemaModelAggregate.JsonSchemaModel", null)
                        .WithMany("Properties")
                        .HasForeignKey("JsonSchemaModelId");
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.JsonSchemaModelAggregate.JsonSchemaModel", b =>
                {
                    b.HasOne("ContentService.Core.AggregateModel.FormConfigAggregate.FormConfig", "FormConfig")
                        .WithMany()
                        .HasForeignKey("FormConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormConfig");
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.FormConfigAggregate.FormConfig", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("ContentService.Core.AggregateModel.JsonSchemaModelAggregate.JsonSchemaModel", b =>
                {
                    b.Navigation("Content");

                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
