using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArtSchools.Migrations
{
    /// <inheritdoc />
    public partial class nullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "working_hours_start",
                schema: "schools",
                table: "schools",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "working_hours_end",
                schema: "schools",
                table: "schools",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<int>(
                name: "working_days_start",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "working_days_end",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "lunch_start",
                schema: "schools",
                table: "schools",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "lunch_end",
                schema: "schools",
                table: "schools",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.InsertData(
                schema: "schools",
                table: "region",
                columns: new[] { "id", "name_en", "name_oz", "name_ru", "name_uz" },
                values: new object[,]
                {
                    { 1, "Tashkent City", "Toshkent shahar", "Город Ташкент", "Тошкент шаҳри" },
                    { 2, "Fergana", "Farg‘ona viloyati", "Ферганская область", "Фарғона вилояти" },
                    { 3, "Andijan", "Andijon viloyati", "Андижанская область", "Андижон вилояти" },
                    { 4, "Bukhara", "Buxoro viloyati", "Бухарская область", "Бухоро вилояти" },
                    { 5, "Jizzakh", "Jizzax viloyati", "Джизакская область", "Жиззах вилояти" },
                    { 6, "Qashqadaryo", "Qashqadaryo viloyati", "Кашкадарьинская область", "Қашқадарё вилояти" },
                    { 7, "Navoiy", "Navoiy viloyati", "Навоийская область", "Навоий вилояти" },
                    { 8, "Namangan", "Namangan viloyati", "Наманганская область", "Наманган вилояти" },
                    { 9, "Samarqand", "Samarqand viloyati", "Самаркандская область", "Самарқанд вилояти" },
                    { 10, "Surxondaryo", "Surxondaryo viloyati", "Сурхандарьинская область", "Сурхондарё вилояти" },
                    { 11, "Sirdaryo", "Sirdaryo viloyati", "Сырдарьинская область", "Сирдарё вилояти" },
                    { 12, "Tashkent", "Toshkent viloyati", "Ташкентская область", "Тошкент вилояти" },
                    { 13, "Khorezm", "Xorazm viloyati", "Хорезмская область", "Хоразм вилояти" },
                    { 14, "Karakalpakstan", "Qoraqalpog‘iston Respublikasi", "Республика Каракалпакстан", "Қорақалпоғистон Республикаси" }
                });

            migrationBuilder.InsertData(
                schema: "schools",
                table: "district",
                columns: new[] { "id", "name_en", "name_oz", "name_ru", "name_uz", "region_id" },
                values: new object[,]
                {
                    { 1, "Bektemir District", "Bektemir tumani", "Бектемирский район", "Бектемир тумани", 1 },
                    { 2, "Chilonzor District", "Chilonzor tumani", "Чиланзарский район", "Чиланзар тумани", 1 },
                    { 3, "Yashnobod District", "Yashnobod tumani", "Яшнабадский район", "Яшнобод тумани", 1 },
                    { 4, "Mirobod District", "Mirobod tumani", "Мирабадский район", "Миробод тумани", 1 },
                    { 5, "Mirzo Ulug‘bek District", "Mirzo Ulug‘bek tumani", "Мирзо-Улугбекский район", "Мирзо Улуғбек тумани", 1 },
                    { 6, "Sergeli District", "Sergeli tumani", "Сергелийский район", "Сергели тумани", 1 },
                    { 7, "Uchtepa District", "Uchtepa tumani", "Учтепинский район", "Учтепа тумани", 1 },
                    { 8, "Shayxontohur District", "Shayxontohur tumani", "Шайхантахурский район", "Шайхонтоҳур тумани", 1 },
                    { 9, "Olmazor District", "Olmazor tumani", "Алмазарский район", "Олмазор тумани", 1 },
                    { 10, "Yunusobod District", "Yunusobod tumani", "Юнусабадский район", "Юнусобод тумани", 1 },
                    { 11, "Yakkasaroy District", "Yakkasaroy tumani", "Яккасарайский район", "Яккасарой тумани", 1 },
                    { 12, "Yangiha’yot District", "Yangiha’yot tumani", "Янгахайотский район", "Янгиҳаёт тумани", 1 },
                    { 13, "Oltiariq District", "Oltiariq tumani", "Алтыарыкский район", "Олтиариқ тумани", 2 },
                    { 14, "Bog‘dod District", "Bog‘dod tumani", "Багдадский район", "Боғдод тумани", 2 },
                    { 15, "Beshariq District", "Beshariq tumani", "Бешарыкский район", "Бешариқ тумани", 2 },
                    { 16, "Buvayda District", "Buvayda tumani", "Бувайдинский район", "Бувайда тумани", 2 },
                    { 17, "Dang‘ara District", "Dang‘ara tumani", "Дангаринский район", "Данғара тумани", 2 },
                    { 18, "Quva District", "Quva tumani", "Кувинский район", "Қува тумани", 2 },
                    { 19, "Qo‘shtepa District", "Qo‘shtepa tumani", "Куштепинский район", "Қўштепа тумани", 2 },
                    { 20, "Rishton District", "Rishton tumani", "Риштанский район", "Риштон тумани", 2 },
                    { 21, "So‘x District", "So‘x tumani", "Сохский район", "Сўх тумани", 2 },
                    { 22, "Toshloq District", "Toshloq tumani", "Ташлакский район", "Тошлоқ тумани", 2 },
                    { 23, "O‘zbekiston District", "O‘zbekiston tumani", "Узбекистанский район", "Ўзбекистон тумани", 2 },
                    { 24, "Uchko‘prik District", "Uchko‘prik tumani", "Учкуприкский район", "Учкўприк тумани", 2 },
                    { 25, "Farg‘ona District", "Farg‘ona tumani", "Ферганский район", "Фарғона тумани", 2 },
                    { 26, "Furqat District", "Furqat tumani", "Фуркатский район", "Фурқат тумани", 1 },
                    { 27, "Yozovon District", "Yozovon tumani", "Язёванский район", "Ёзёвон тумани", 1 }
                });

            migrationBuilder.InsertData(
                schema: "schools",
                table: "schools",
                columns: new[] { "id", "address", "district_id", "email", "is_deleted", "lunch_end", "lunch_start", "name_en", "name_oz", "name_ru", "name_uz", "region_id", "site_link", "type", "working_days_end", "working_days_start", "working_hours_end", "working_hours_start" },
                values: new object[] { 1, null, 5, null, false, null, null, "Madaniyat.uz", "Madaniyat.uz", "Madaniyat.uz", "Madaniyat.uz", 1, null, "MINISTRY", null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "schools",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "district",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "schools",
                table: "region",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "working_hours_start",
                schema: "schools",
                table: "schools",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "working_hours_end",
                schema: "schools",
                table: "schools",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "working_days_start",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "working_days_end",
                schema: "schools",
                table: "schools",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "lunch_start",
                schema: "schools",
                table: "schools",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "lunch_end",
                schema: "schools",
                table: "schools",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);
        }
    }
}
