using Microsoft.EntityFrameworkCore.Migrations;

namespace TestEquipment.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Equipment_EquipmentsId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_EquipmentsId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EquipmentsId",
                table: "Equipment");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentTypeId",
                table: "Equipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentType_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId",
                principalTable: "EquipmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentType_EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentsId",
                table: "Equipment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentsId",
                table: "Equipment",
                column: "EquipmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Equipment_EquipmentsId",
                table: "Equipment",
                column: "EquipmentsId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
