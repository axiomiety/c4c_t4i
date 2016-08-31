var mongoose = require('mongoose');
mongoose.connect('mongodb://localhost/t4i');

var Student = require('../db-api/models/Student');
var student1 = {first_name: 'john', last_name: 'smith', id: 321};

Student.create( student1, function(err, student) {
  if (err) {
    console.error(err);
  } else {
    console.log(student);
  }
});

process.exit();
