'use strict'

var globalObject = {}
module.exports = globalObject

/**
Set Up Logging
 **/

const bunyan = require('bunyan')
const log = globalObject.log = bunyan.createLogger({
  name: 'tfiapp'
  , streams: [{
    level: 'trace'
    , path: `${__dirname}/logs/trace.log`
  }
  , {
    level: 'debug',
    stream: process.stdout
  }
  , {
    level: 'info',
    path: `${__dirname}/logs/info.log`
  }
  , {
    level: 'warn',
    path: `${__dirname}/logs/warn.log`
  }
  , {
    level: 'error',
    path: `${__dirname}/logs/error.log`
  }
  , {
    level: 'fatal',
    path: `${__dirname}/logs/fatal.log`
  }]
})
globalObject.routeLog = bunyan.createLogger({
  name: 'tfiapp'
  , streams: [{
    level: 'trace',
    path: `${__dirname}/logs/access.log`
  }]
})

/**
Prepare Express App
 **/

var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

var routes = require('./routes/index');

var app = express();

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');

// uncomment after placing your favicon in /public
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', routes);

// catch 404 and forward to error handler
app.use(function(req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
  app.use(function(err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', {
      message: err.message,
      error: err
    });
  });
}

// production error handler
// no stacktraces leaked to user
app.use(function(err, req, res, next) {
  res.status(err.status || 500);
  res.render('error', {
    message: err.message,
    error: {}
  });
});

/**
Connect To Database - mongo
 **/

/*var mongoose = require('mongoose');
mongoose.Promise = global.Promise;
mongoose.connect('mongodb://localhost:27017/t4i')
  .then(() =>  console.log('mongodb connection to t4i db succesful'))
  .catch((err) => console.error(err));
*/
  
/**
Firebase
**/

var firebase = require("firebase");
// Initialize Firebase
var config = {
  apiKey: "AIzaSyBNUiwOjLnILeorZzGbZ-Gy787zCfi0Ef8",
  authDomain: "c4c-t4i.firebaseapp.com",
  databaseURL: "https://c4c-t4i.firebaseio.com",
  storageBucket: "c4c-t4i.appspot.com"
};
firebase.initializeApp(config);

var provider = new firebase.auth.GoogleAuthProvider();

function isUserEqual(googleUser, firebaseUser) {
  if (firebaseUser) {
    var providerData = firebaseUser.providerData;
    for (var i = 0; i < providerData.length; i++) {
      if (providerData[i].providerId === firebase.auth.GoogleAuthProvider.PROVIDER_ID &&
          providerData[i].uid === googleUser.getBasicProfile().getId()) {
        // We don't need to reauth the Firebase connection.
        return true;
      }
    }
  }
  return false;
}

function onSignIn(googleUser) {
  console.log('Google Auth Response', googleUser);
  // We need to register an Observer on Firebase Auth to make sure auth is initialized.
  var unsubscribe = firebase.auth().onAuthStateChanged(function(firebaseUser) {
    unsubscribe();
    // Check if we are already signed-in Firebase with the correct user.
    if (!isUserEqual(googleUser, firebaseUser)) {
      // Build Firebase credential with the Google ID token.
      var credential = firebase.auth.GoogleAuthProvider.credential(
          googleUser.getAuthResponse().id_token);
      // Sign in with credential from the Google user.
      firebase.auth().signInWithCredential(credential).catch(function(error) {
        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;
        // The email of the user's account used.
        var email = error.email;
        // The firebase.auth.AuthCredential type that was used.
        var credential = error.credential;
        // ...
      });
    } else {
      console.log('User already signed-in Firebase.');
    }
  });
}

module.exports = app;
