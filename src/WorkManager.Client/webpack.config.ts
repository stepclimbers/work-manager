import * as webpack from 'webpack';
// import * as bundleAnalyzer from "webpack-bundle-analyzer";
import * as path from 'path';

// const { BundleAnalyzerPlugin } = bundleAnalyzer;

const config: webpack.Configuration = {
    entry: [
        // "babel-polyfill",
        './Components/index.tsx'
        // "./foo.ts"
    ],
    mode: 'development',
    output: {
        path: path.resolve('./build'), // without path.resolve, default url is absolute
        publicPath: '/build',
        filename: 'bundle.js'
        // libraryTarget: "umd"
    },
    watch: true,
    optimization: {
        removeAvailableModules: false,
        removeEmptyChunks: false,
        splitChunks: false
    },
    devtool: 'cheap-module-eval-source-map',
    plugins: [
        // new BundleAnalyzerPlugin({})
    ],
    cache: true,
    module: {
        rules: [
            {
                test: /\.(js|jsx|tsx|ts)$/,
                exclude: /node_modules/,
                loader: 'babel-loader'
            },
            {
                test: /\.css$/,
                use: [{ loader: 'style-loader' }, { loader: 'css-loader' }]
                // loader: "style-loader!css-loader",
            },
            {
                test: /\.(png|jpg|gif)$/,
                use: [
                    {
                        loader: 'file-loader'
                    }
                ]
            },
            {
                test: /\.(ts|tsx|js)$/,
                exclude: /node_modules/,
                loader: 'eslint-loader',
                enforce: 'pre',
                options: {
                    failOnWarning: false,
                    failOnError: true
                }
            }
        ]
    },
    resolve: {
        extensions: [
            '*',
            '.mjs',
            '.js',
            '.jsx',
            '.ts',
            '.tsx',
            '.css',
            'png',
            'jpg',
            'gif'
        ]
    }
    // externals: {
    //   react: "React",
    //   "react-dom": "ReactDOM"
    //   // 'react-router': 'ReactRouter',
    // }
};

export default config;
