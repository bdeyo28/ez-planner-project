using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerAPI.Migrations
{
    public partial class AddedAttendeeEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendee_Event_EventId",
                table: "Attendee");

            migrationBuilder.DropForeignKey(
                name: "FK_EventActivities_Activity_ActivityId",
                table: "EventActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EventActivities_Event_EventId",
                table: "EventActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendees_Attendee_AttendeeId",
                table: "EventAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendees_Event_EventId",
                table: "EventAttendees");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerId",
                table: "EventOrganizer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventOrganizer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventAttendees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AttendeeId",
                table: "EventAttendees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventActivities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "EventActivities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Attendee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventOrganizer_EventId",
                table: "EventOrganizer",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventOrganizer_OrganizerId",
                table: "EventOrganizer",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendee_Event_EventId",
                table: "Attendee",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventActivities_Activity_ActivityId",
                table: "EventActivities",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventActivities_Event_EventId",
                table: "EventActivities",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendees_Attendee_AttendeeId",
                table: "EventAttendees",
                column: "AttendeeId",
                principalTable: "Attendee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendees_Event_EventId",
                table: "EventAttendees",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventOrganizer_Event_EventId",
                table: "EventOrganizer",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventOrganizer_Organizer_OrganizerId",
                table: "EventOrganizer",
                column: "OrganizerId",
                principalTable: "Organizer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendee_Event_EventId",
                table: "Attendee");

            migrationBuilder.DropForeignKey(
                name: "FK_EventActivities_Activity_ActivityId",
                table: "EventActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EventActivities_Event_EventId",
                table: "EventActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendees_Attendee_AttendeeId",
                table: "EventAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendees_Event_EventId",
                table: "EventAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_EventOrganizer_Event_EventId",
                table: "EventOrganizer");

            migrationBuilder.DropForeignKey(
                name: "FK_EventOrganizer_Organizer_OrganizerId",
                table: "EventOrganizer");

            migrationBuilder.DropIndex(
                name: "IX_EventOrganizer_EventId",
                table: "EventOrganizer");

            migrationBuilder.DropIndex(
                name: "IX_EventOrganizer_OrganizerId",
                table: "EventOrganizer");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerId",
                table: "EventOrganizer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventOrganizer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventAttendees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttendeeId",
                table: "EventAttendees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventActivities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "EventActivities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Attendee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendee_Event_EventId",
                table: "Attendee",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventActivities_Activity_ActivityId",
                table: "EventActivities",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventActivities_Event_EventId",
                table: "EventActivities",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendees_Attendee_AttendeeId",
                table: "EventAttendees",
                column: "AttendeeId",
                principalTable: "Attendee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendees_Event_EventId",
                table: "EventAttendees",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
