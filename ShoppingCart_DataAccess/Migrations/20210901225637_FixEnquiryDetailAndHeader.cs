using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart_DataAccess.Migrations
{
    public partial class FixEnquiryDetailAndHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryDetail_InquiryHeader_InqueryHeaderId",
                table: "InquiryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_InquiryDetail_InquiryHeader_ProductId",
                table: "InquiryDetail");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "InquiryHeader",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InqueryHeaderId",
                table: "InquiryDetail",
                newName: "InquiryHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_InquiryDetail_InqueryHeaderId",
                table: "InquiryDetail",
                newName: "IX_InquiryDetail_InquiryHeaderId");

            migrationBuilder.AddColumn<DateTime>(
                name: "InquiryDate",
                table: "InquiryHeader",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryDetail_InquiryHeader_InquiryHeaderId",
                table: "InquiryDetail",
                column: "InquiryHeaderId",
                principalTable: "InquiryHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryDetail_Product_ProductId",
                table: "InquiryDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InquiryDetail_InquiryHeader_InquiryHeaderId",
                table: "InquiryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_InquiryDetail_Product_ProductId",
                table: "InquiryDetail");

            migrationBuilder.DropColumn(
                name: "InquiryDate",
                table: "InquiryHeader");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InquiryHeader",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "InquiryHeaderId",
                table: "InquiryDetail",
                newName: "InqueryHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_InquiryDetail_InquiryHeaderId",
                table: "InquiryDetail",
                newName: "IX_InquiryDetail_InqueryHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryDetail_InquiryHeader_InqueryHeaderId",
                table: "InquiryDetail",
                column: "InqueryHeaderId",
                principalTable: "InquiryHeader",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InquiryDetail_InquiryHeader_ProductId",
                table: "InquiryDetail",
                column: "ProductId",
                principalTable: "InquiryHeader",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
