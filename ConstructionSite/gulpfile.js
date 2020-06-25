var gulp = require("gulp");

var paths = {
    webroot = "./wwwroot/"
};

gulp.task("bootstrap_min_css", x => {
    return gulp.src("node_modules/bootstrap/dist/css/bootstrap.min.css")
        .pipe(gulp.dest("wwwroot/lib/bootstrap/css"));
});