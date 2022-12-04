const gulp = require("gulp");
const {run} = require('gulp-dotnet-cli');
const ts = require("gulp-typescript");
const webpack = require('webpack-stream')
const tsProject = ts.createProject("tsconfig.json");
const shell = require('gulp-shell');

gulp.task("serve-dev", () => {
    return gulp.src('./').pipe(shell((['npm run start'])))
});
gulp.task("http", () => {
    return gulp.src('../WebServer/WebServer/WebServer.csproj', {read: false}).pipe(run())
});
gulp.task("default", gulp.series(gulp.parallel("http", "serve-dev")));
