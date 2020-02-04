using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestEquipment.Migrations
{
    public partial class changetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "EquipmentType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "EquipmentType",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EquipmentType",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "EquipmentType");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "EquipmentType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EquipmentType");
        }
    }
}
