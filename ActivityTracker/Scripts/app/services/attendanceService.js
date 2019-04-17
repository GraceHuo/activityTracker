var AttendanceService = function () {
    var createAttendance = function (activityId, done, fail) {
        $.post("/api/attendances", { activityId: activityId })
            .done(done)
            .fail(fail);
    }

    var deleteAttendance = function (activityId, done, fail) {
        $.ajax({
                url: "/api/attendances/" + activityId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    }

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();