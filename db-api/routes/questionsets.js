var express = require('express');
var router = express.Router();
var mongoose = require('mongoose');
var QuestionSet = require('../models/QuestionSet');
router.get('/', function(req, res, next) {
  QuestionSet.find(function (err, qs) {
    if (err) return next(err);
    res.json(qs);
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
