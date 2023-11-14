using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b716d3d-b9ef-4876-a7e0-b01de6af3ff1", "AQAAAAEAACcQAAAAEGypD2IVruVb85rBFOqsShe/Wmd2ua+n0hmT5FppiMq+Q2XV+belsl76o/PvUmyjAQ==", "497b1534-767c-4f84-8c14-3d77289ebdfc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "796f18a9-9b5a-43e8-9657-a85cd34ec1d3", "AQAAAAEAACcQAAAAEI/myt5VuykEkiVtlP5699T1DeubTTsECKOILIiU59aj5kKovZ082pTGkwrb5VCg6A==", "1edd9e62-8620-4f18-b60f-d65fd39c7ace" });
        }
    }
}
