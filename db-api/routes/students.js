var express = require('express');
var router = express.Router();
var mongoose = require('mongoose');
var Student = require('../models/Student');
router.get('/', function(req, res, next) {
  Student.find(function (err, students) {
    if (err) return next(err);
    res.json(students);
  });
});
module.exports = router;
/*
router.get('/:id', function(req, res, next) {
  Student.findById(req.params.id, function (err, post) {
    if (err) return next(err);
    res.json(post);
  });
});
*/
