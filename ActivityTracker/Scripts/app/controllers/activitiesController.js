var ActivitiesController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        var activityId = button.attr("data-activity-id");
        
        if (button.hasClass("btn-default")) {
            attendanceService.createAttendance(activityId, done, fail);
        }
        else {
            attendanceService.deleteAttendance(activityId, done, fail);
        }
    }

    var fail = function () {
        alert("Something failed!");
    };

    var done = function () {
        var text = (button.text().trim() == "Going") ? "Going?" : "Going";
        button
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(text);
    };

    return {
        init: init
    }
}(AttendanceService);