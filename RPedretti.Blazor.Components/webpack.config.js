const path = require("path");
const webpack = require("webpack");

module.exports = {
    mode: 'production',
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
        "suggest-box": "./ts/SuggestBox.ts",
        "init-samespaces": "./ts/InitNamespaces.ts"
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