import CleanWebpackTemplate from "clean-webpack-plugin";
import HtmlWebpackPlugin from "html-webpack-plugin";
import * as path from "path";
import TslintWebpackPlugin from "tslint-webpack-plugin";
import { VueLoaderPlugin } from "vue-loader";
import * as webpack from "webpack";

const indexHtmlBody = "<div id='app'></div>";

const indexPluginOptions: HtmlWebpackPlugin.Options = {
  bodyHtmlSnippet: indexHtmlBody,
  filename: "index.html",
  inject: false,
  links: [
    // "https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css",
    // "https://cdnjs.cloudflare.com/ajax/libs/jsoneditor/5.19.2/jsoneditor.min.css",
    // "https://unpkg.com/element-ui/lib/theme-chalk/index.css",
  ],
  minify: false,
  template: require("html-webpack-template"),
  title: "JsonSafe",
};

const isProduction = Boolean(
  process.env.npm_lifecycle_event || "".match(/:prod/),
);

const config: webpack.Configuration = {
  devtool: "#eval-source-map",
  entry: "./src/main.ts",
  module: {
    rules: [
      {
        enforce: "pre",
        test: /\.ts$/,
        use: [
          {
            loader: "tslint-loader",
            options: {
              emitErrors: true,
              failOnHint: true,
             },
          },
        ],
      },
      {
        exclude: /node_modules/,
        loader: "ts-loader",
        options: {
          appendTsSuffixTo: [/\.vue$/],
          compiler: "ttypescript",
        },
        test: /\.tsx?$/,
      },
      {
        loader: "vue-loader",
        options: {},
        test: /\.vue$/,
      },
      {
        test: /\.less$/,
        use: ["vue-style-loader", "css-loader", "less-loader"],
      },
      {
        test: /\.css$/,
        use: ["style-loader", "css-loader"],
      },
      {
        loader: "url-loader?limit=30000&name=[name]-[hash].[ext]",
        test: /\.(eot|woff|woff2|ttf|svg|png|jpg)$/,
      },
    ],
  },
  name: "app",
  output: {
    filename: "[name].[chunkhash].js",
    path: path.resolve(__dirname, "../JsonSafe.WebApi/wwwroot"),
  },
  performance: {
    hints: false,
  },
  plugins: [
    new TslintWebpackPlugin({
      files: ["./src/**/*.ts"],
    }),
    new CleanWebpackTemplate("wwwroot", {
      root: path.resolve(__dirname, "../JsonSafe.WebApi/"),
    }),
    new VueLoaderPlugin(),
  ],
  resolve: {
    alias: {
      vue$: "vue/dist/vue.esm.js",
    },
    extensions: [".ts", ".js", ".vue", ".json"],
  },
};

if (isProduction) {
  indexPluginOptions.minify = {
    collapseWhitespace: true,
    html5: true,
    useShortDoctype: true,
  };
}

const plugin = new HtmlWebpackPlugin(indexPluginOptions);
if (config.plugins) {
  config.plugins.push(plugin);
} else {
  config.plugins = [plugin];
}

export default config;
