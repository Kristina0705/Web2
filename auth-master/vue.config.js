const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
  outputDir: "./src/main/resources/public",
  devServer: {
    proxy: "http://localhost:8080",
    port: 4200,
  },
});
