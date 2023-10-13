using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ACC");

            migrationBuilder.EnsureSchema(
                name: "LO");

            migrationBuilder.EnsureSchema(
                name: "EX");

            migrationBuilder.EnsureSchema(
                name: "BL");

            migrationBuilder.EnsureSchema(
                name: "SAL");

            migrationBuilder.EnsureSchema(
                name: "CO");

            migrationBuilder.EnsureSchema(
                name: "RE");

            migrationBuilder.EnsureSchema(
                name: "CU");

            migrationBuilder.EnsureSchema(
                name: "BPR");

            migrationBuilder.EnsureSchema(
                name: "PRO");

            migrationBuilder.EnsureSchema(
                name: "MO");

            migrationBuilder.EnsureSchema(
                name: "FIN");

            migrationBuilder.EnsureSchema(
                name: "PW");

            migrationBuilder.EnsureSchema(
                name: "TE");

            migrationBuilder.EnsureSchema(
                name: "WO");

            migrationBuilder.CreateTable(
                name: "Accesses",
                schema: "ACC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeOfEducation = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    LastLoginDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ForgetCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendForgetCodeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Representative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                schema: "BL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CooperationRequests",
                schema: "RE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Courses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CooperationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CounselingRequests",
                schema: "RE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Courses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounselingRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseCategories",
                schema: "CU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                schema: "BPR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCheck = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                schema: "EX",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    ExamTime = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQS",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenderProductTypes",
                schema: "BPR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationCode_logs",
                schema: "ACC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IdentificationCodeRuleId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationCode_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationCodeRules",
                schema: "ACC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForOwner = table.Column<bool>(type: "bit", nullable: false),
                    ForUser = table.Column<bool>(type: "bit", nullable: false),
                    ForHasDisCount = table.Column<bool>(type: "bit", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOffPrice = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationCodeRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                schema: "BPR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "CO",
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieCategories",
                schema: "MO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferZones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferZones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderPriceLogs",
                schema: "SAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PriceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPriceLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    SuperSet = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    NumberSet = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletionTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostCategories",
                schema: "BL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentPostCategoryId = table.Column<int>(type: "int", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCategories_PostCategories_ParentPostCategoryId",
                        column: x => x.ParentPostCategoryId,
                        principalSchema: "BL",
                        principalTable: "PostCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceCenters",
                schema: "FIN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pricings",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                schema: "LO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteRules",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedIdUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeSizes",
                schema: "PW",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeWarehouse = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                schema: "TE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Expert = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                schema: "FIN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowHistories",
                schema: "WO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    ApproveId = table.Column<int>(type: "int", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlowHistories_AspNetUsers_ApproveId",
                        column: x => x.ApproveId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkFlows",
                schema: "WO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFlows_AspNetUsers_ApproveId",
                        column: x => x.ApproveId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                schema: "BL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogCategoryId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sumary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AparatID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AparatLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalSchema: "BL",
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailsItems",
                schema: "BPR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTopic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailsId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailsItems_Details_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "BPR",
                        principalTable: "Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Examers",
                schema: "EX",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsStart = table.Column<bool>(type: "bit", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrueQuestion = table.Column<int>(type: "int", nullable: false),
                    FalseQueston = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examers_Exams_ExamId",
                        column: x => x.ExamId,
                        principalSchema: "EX",
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestions",
                schema: "EX",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    QuestionLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    choiceA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    choiceB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    choiceC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    choiceD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamQuestions_Exams_ExamId",
                        column: x => x.ExamId,
                        principalSchema: "EX",
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreTypes",
                schema: "BPR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderProductTypeId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreTypes_GenderProductTypes_GenderProductTypeId",
                        column: x => x.GenderProductTypeId,
                        principalSchema: "BPR",
                        principalTable: "GenderProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubMenus",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "CO",
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubMenus_SubMenus_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "CO",
                        principalTable: "SubMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "MO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AparatID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AparatLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberShow = table.Column<int>(type: "int", nullable: false),
                    Rates = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_MovieCategories_MovieCategoryId",
                        column: x => x.MovieCategoryId,
                        principalSchema: "MO",
                        principalTable: "MovieCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfferTypeId = table.Column<int>(type: "int", nullable: false),
                    OfferZoneId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_OfferTypes_OfferTypeId",
                        column: x => x.OfferTypeId,
                        principalTable: "OfferTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_OfferZones_OfferZoneId",
                        column: x => x.OfferZoneId,
                        principalTable: "OfferZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "BL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCategoryId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_PostCategories_PostCategoryId",
                        column: x => x.PostCategoryId,
                        principalSchema: "BL",
                        principalTable: "PostCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceCenterItems",
                schema: "FIN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PriceCenterId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceCenterItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceCenterItems_PriceCenters_PriceCenterId",
                        column: x => x.PriceCenterId,
                        principalSchema: "FIN",
                        principalTable: "PriceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "LO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "LO",
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypeSizeItems",
                schema: "PW",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeSizeId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSizeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeSizeItems_TypeSizes_TypeSizeId",
                        column: x => x.TypeSizeId,
                        principalSchema: "PW",
                        principalTable: "TypeSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WalletHistories",
                schema: "FIN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletHistories_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "FIN",
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerSheets",
                schema: "EX",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    ExamerId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserAnswer = table.Column<int>(type: "int", nullable: false),
                    TrueAnswer = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerSheets_Examers_ExamerId",
                        column: x => x.ExamerId,
                        principalSchema: "EX",
                        principalTable: "Examers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerSheets_ExamQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "EX",
                        principalTable: "ExamQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Guides",
                schema: "PRO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreTypeId = table.Column<int>(type: "int", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guides_StoreTypes_StoreTypeId",
                        column: x => x.StoreTypeId,
                        principalSchema: "BPR",
                        principalTable: "StoreTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                schema: "BPR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreTypeId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypes_StoreTypes_StoreTypeId",
                        column: x => x.StoreTypeId,
                        principalSchema: "BPR",
                        principalTable: "StoreTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreTypeDetails",
                schema: "BPR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreTypeId = table.Column<int>(type: "int", nullable: false),
                    DetailsId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTypeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreTypeDetails_Details_DetailsId",
                        column: x => x.DetailsId,
                        principalSchema: "BPR",
                        principalTable: "Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreTypeDetails_StoreTypes_StoreTypeId",
                        column: x => x.StoreTypeId,
                        principalSchema: "BPR",
                        principalTable: "StoreTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "LO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plaque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "LO",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "LO",
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "PRO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CurrentImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SizeGuideId = table.Column<int>(type: "int", nullable: true),
                    GuideId = table.Column<int>(type: "int", nullable: true),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    StoreTypeId = table.Column<int>(type: "int", nullable: false),
                    TypeSizeId = table.Column<int>(type: "int", nullable: true),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Guides_GuideId",
                        column: x => x.GuideId,
                        principalSchema: "PRO",
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "BPR",
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_StoreTypes_StoreTypeId",
                        column: x => x.StoreTypeId,
                        principalSchema: "BPR",
                        principalTable: "StoreTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_TypeSizes_TypeSizeId",
                        column: x => x.TypeSizeId,
                        principalSchema: "PW",
                        principalTable: "TypeSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "CO",
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "CU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCategoryId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodOfTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasExam = table.Column<bool>(type: "bit", nullable: false),
                    HasDegree = table.Column<bool>(type: "bit", nullable: false),
                    NumberStudent = table.Column<int>(type: "int", nullable: false),
                    language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseLevel = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseCategories_CourseCategoryId",
                        column: x => x.CourseCategoryId,
                        principalSchema: "CU",
                        principalTable: "CourseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    TypeOffPrice = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: true),
                    KeyDiscountPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForAll = table.Column<bool>(type: "bit", nullable: true),
                    PermittedNumber = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                schema: "PRO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorites_Products_productId",
                        column: x => x.productId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferItems_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Price_Logs",
                schema: "FIN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Price_Logs_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                schema: "BPR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DetailsItemId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_DetailsItems_DetailsItemId",
                        column: x => x.DetailsItemId,
                        principalSchema: "BPR",
                        principalTable: "DetailsItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductWareHouses",
                schema: "PW",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TypeSizeItemId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWareHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductWareHouses_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductWareHouses_TypeSizeItems_TypeSizeItemId",
                        column: x => x.TypeSizeItemId,
                        principalSchema: "PW",
                        principalTable: "TypeSizeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ViewPages",
                schema: "CO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewPages_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "BL",
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViewPages_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCourses",
                schema: "TE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "CU",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "TE",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCurricula",
                schema: "TE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFull = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCurricula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherCurricula_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "CU",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherCurricula_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "TE",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscountCodeUseds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodeUseds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountCodeUseds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountCodeUseds_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "SAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacPart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FactorType = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: true),
                    IsFinaly = table.Column<bool>(type: "bit", nullable: false),
                    TotalDiscount = table.Column<double>(type: "float", nullable: false),
                    TotalExtraAmount = table.Column<double>(type: "float", nullable: false),
                    TotalPayment = table.Column<double>(type: "float", nullable: false),
                    FinalPayment = table.Column<double>(type: "float", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    IsProductReference = table.Column<bool>(type: "bit", nullable: true),
                    newPrice = table.Column<float>(type: "real", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "LO",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "SAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    FinalAmount = table.Column<double>(type: "float", nullable: false),
                    ProductWareHouseId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_ProductWareHouses_ProductWareHouseId",
                        column: x => x.ProductWareHouseId,
                        principalSchema: "PW",
                        principalTable: "ProductWareHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recharges",
                schema: "PRO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductWareHouseId = table.Column<int>(type: "int", nullable: false),
                    PhonNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCheck = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recharges_ProductWareHouses_ProductWareHouseId",
                        column: x => x.ProductWareHouseId,
                        principalSchema: "PW",
                        principalTable: "ProductWareHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCurriculumTimes",
                schema: "TE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherCurriculumId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCurriculumTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherCurriculumTimes_TeacherCurricula_TeacherCurriculumId",
                        column: x => x.TeacherCurriculumId,
                        principalSchema: "TE",
                        principalTable: "TeacherCurricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: "SAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductWareHouseId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    MainPriceId = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "SAL",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Price_Logs_MainPriceId",
                        column: x => x.MainPriceId,
                        principalSchema: "FIN",
                        principalTable: "Price_Logs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "PRO",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ProductWareHouses_ProductWareHouseId",
                        column: x => x.ProductWareHouseId,
                        principalSchema: "PW",
                        principalTable: "ProductWareHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestHeaders",
                schema: "RE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    TeacherCurriculumId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestHeaders_AspNetUsers_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestHeaders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "SAL",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestHeaders_TeacherCurricula_TeacherCurriculumId",
                        column: x => x.TeacherCurriculumId,
                        principalSchema: "TE",
                        principalTable: "TeacherCurricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestHeaders_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "TE",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                schema: "LO",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProvinceId",
                schema: "LO",
                table: "Addresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                schema: "LO",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSheets_ExamerId",
                schema: "EX",
                table: "AnswerSheets",
                column: "ExamerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSheets_QuestionId",
                schema: "EX",
                table: "AnswerSheets",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogCategoryId",
                schema: "BL",
                table: "Blogs",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                schema: "SAL",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductWareHouseId",
                schema: "SAL",
                table: "Carts",
                column: "ProductWareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                schema: "LO",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                schema: "CO",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                schema: "CO",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                schema: "CO",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseCategoryId",
                schema: "CU",
                table: "Courses",
                column: "CourseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProductId",
                schema: "CU",
                table: "Courses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsItems_DetailsId",
                schema: "BPR",
                table: "DetailsItems",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodeUseds_DiscountId",
                table: "DiscountCodeUseds",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodeUseds_UserId",
                table: "DiscountCodeUseds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductId",
                table: "Discounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Examers_ExamId",
                schema: "EX",
                table: "Examers",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Examers_UserId",
                schema: "EX",
                table: "Examers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_ExamId",
                schema: "EX",
                table: "ExamQuestions",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_productId",
                schema: "PRO",
                table: "Favorites",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                schema: "PRO",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_StoreTypeId",
                schema: "PRO",
                table: "Guides",
                column: "StoreTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                schema: "CO",
                table: "Menus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieCategoryId",
                schema: "MO",
                table: "Movies",
                column: "MovieCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferItems_OfferId",
                table: "OfferItems",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferItems_ProductId",
                table: "OfferItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferTypeId",
                table: "Offers",
                column: "OfferTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferZoneId",
                table: "Offers",
                column: "OfferZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_DiscountId",
                schema: "SAL",
                table: "OrderDetails",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_MainPriceId",
                schema: "SAL",
                table: "OrderDetails",
                column: "MainPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                schema: "SAL",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                schema: "SAL",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductWareHouseId",
                schema: "SAL",
                table: "OrderDetails",
                column: "ProductWareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                schema: "SAL",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountId",
                schema: "SAL",
                table: "Orders",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "SAL",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_ParentPostCategoryId",
                schema: "BL",
                table: "PostCategories",
                column: "ParentPostCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostCategoryId",
                schema: "BL",
                table: "Posts",
                column: "PostCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Price_Logs_ProductId",
                schema: "FIN",
                table: "Price_Logs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceCenterItems_PriceCenterId",
                schema: "FIN",
                table: "PriceCenterItems",
                column: "PriceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_DetailsItemId",
                schema: "BPR",
                table: "ProductDetails",
                column: "DetailsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                schema: "BPR",
                table: "ProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_GuideId",
                schema: "PRO",
                table: "Products",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                schema: "PRO",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreTypeId",
                schema: "PRO",
                table: "Products",
                column: "StoreTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeSizeId",
                schema: "PRO",
                table: "Products",
                column: "TypeSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_StoreTypeId",
                schema: "BPR",
                table: "ProductTypes",
                column: "StoreTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWareHouses_ProductId",
                schema: "PW",
                table: "ProductWareHouses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWareHouses_TypeSizeItemId",
                schema: "PW",
                table: "ProductWareHouses",
                column: "TypeSizeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Recharges_ProductWareHouseId",
                schema: "PRO",
                table: "Recharges",
                column: "ProductWareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestHeaders_ApplicantId",
                schema: "RE",
                table: "RequestHeaders",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestHeaders_OrderId",
                schema: "RE",
                table: "RequestHeaders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestHeaders_TeacherCurriculumId",
                schema: "RE",
                table: "RequestHeaders",
                column: "TeacherCurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestHeaders_TeacherId",
                schema: "RE",
                table: "RequestHeaders",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreTypeDetails_DetailsId",
                schema: "BPR",
                table: "StoreTypeDetails",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreTypeDetails_StoreTypeId",
                schema: "BPR",
                table: "StoreTypeDetails",
                column: "StoreTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreTypes_GenderProductTypeId",
                schema: "BPR",
                table: "StoreTypes",
                column: "GenderProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenus_MenuId",
                schema: "CO",
                table: "SubMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenus_ParentId",
                schema: "CO",
                table: "SubMenus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_CourseId",
                schema: "TE",
                table: "TeacherCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_TeacherId",
                schema: "TE",
                table: "TeacherCourses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCurricula_CourseId",
                schema: "TE",
                table: "TeacherCurricula",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCurricula_TeacherId",
                schema: "TE",
                table: "TeacherCurricula",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCurriculumTimes_TeacherCurriculumId",
                schema: "TE",
                table: "TeacherCurriculumTimes",
                column: "TeacherCurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                schema: "TE",
                table: "Teachers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeSizeItems_TypeSizeId",
                schema: "PW",
                table: "TypeSizeItems",
                column: "TypeSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewPages_BlogId",
                schema: "CO",
                table: "ViewPages",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewPages_ProductId",
                schema: "CO",
                table: "ViewPages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistories_WalletId",
                schema: "FIN",
                table: "WalletHistories",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                schema: "FIN",
                table: "Wallets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowHistories_ApproveId",
                schema: "WO",
                table: "WorkFlowHistories",
                column: "ApproveId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlows_ApproveId",
                schema: "WO",
                table: "WorkFlows",
                column: "ApproveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accesses",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "AnswerSheets",
                schema: "EX");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "Comments",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "CooperationRequests",
                schema: "RE");

            migrationBuilder.DropTable(
                name: "CounselingRequests",
                schema: "RE");

            migrationBuilder.DropTable(
                name: "DiscountCodeUseds");

            migrationBuilder.DropTable(
                name: "Emails",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "FAQS",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "Favorites",
                schema: "PRO");

            migrationBuilder.DropTable(
                name: "IdentificationCode_logs",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "IdentificationCodeRules",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "Images",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "Keywords",
                schema: "BPR");

            migrationBuilder.DropTable(
                name: "Movies",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "OfferItems");

            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "OrderPriceLogs",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "PlanCategories");

            migrationBuilder.DropTable(
                name: "PlanItems");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Portfolios",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "BL");

            migrationBuilder.DropTable(
                name: "PriceCenterItems",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "Pricings",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "ProductDetails",
                schema: "BPR");

            migrationBuilder.DropTable(
                name: "Recharges",
                schema: "PRO");

            migrationBuilder.DropTable(
                name: "RequestHeaders",
                schema: "RE");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "SiteRules",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "Sliders",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "StoreTypeDetails",
                schema: "BPR");

            migrationBuilder.DropTable(
                name: "SubMenus",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "TeacherCourses",
                schema: "TE");

            migrationBuilder.DropTable(
                name: "TeacherCurriculumTimes",
                schema: "TE");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "ViewPages",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "WalletHistories",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "WorkFlowHistories",
                schema: "WO");

            migrationBuilder.DropTable(
                name: "WorkFlows",
                schema: "WO");

            migrationBuilder.DropTable(
                name: "Examers",
                schema: "EX");

            migrationBuilder.DropTable(
                name: "ExamQuestions",
                schema: "EX");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MovieCategories",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Price_Logs",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PostCategories",
                schema: "BL");

            migrationBuilder.DropTable(
                name: "PriceCenters",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DetailsItems",
                schema: "BPR");

            migrationBuilder.DropTable(
                name: "ProductWareHouses",
                schema: "PW");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "CO");

            migrationBuilder.DropTable(
                name: "TeacherCurricula",
                schema: "TE");

            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "BL");

            migrationBuilder.DropTable(
                name: "Wallets",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "Exams",
                schema: "EX");

            migrationBuilder.DropTable(
                name: "OfferTypes");

            migrationBuilder.DropTable(
                name: "OfferZones");

            migrationBuilder.DropTable(
                name: "Details",
                schema: "BPR");

            migrationBuilder.DropTable(
                name: "TypeSizeItems",
                schema: "PW");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "LO");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "CU");

            migrationBuilder.DropTable(
                name: "Teachers",
                schema: "TE");

            migrationBuilder.DropTable(
                name: "BlogCategories",
                schema: "BL");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "LO");

            migrationBuilder.DropTable(
                name: "CourseCategories",
                schema: "CU");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "PRO");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Provinces",
                schema: "LO");

            migrationBuilder.DropTable(
                name: "Guides",
                schema: "PRO");

            migrationBuilder.DropTable(
                name: "ProductTypes",
                schema: "BPR");

            migrationBuilder.DropTable(
                name: "TypeSizes",
                schema: "PW");

            migrationBuilder.DropTable(
                name: "StoreTypes",
                schema: "BPR");

            migrationBuilder.DropTable(
                name: "GenderProductTypes",
                schema: "BPR");
        }
    }
}
