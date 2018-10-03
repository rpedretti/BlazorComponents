const path = require("path");
const webpack = require("webpack");

module.exports = {
    mode: 'development',
    resolve: {
        extensions: [".ts", ".js"]
    },
    devtool: "inline-source-map",
    module: {
        rules: [
            {
                test: /\.ts?$/,
                loader: "ts-loader"
            }
        ]
    },
    entry: {
        "here-map_v1": "./ts/InitMap.ts"
    },
    output: {
        path: path.join(__dirname, "/content/js"),
        filename: "[name].min.js"
    },
    optimization: {
        splitChunks: {
            chunks: 'all'
        }
    }
};