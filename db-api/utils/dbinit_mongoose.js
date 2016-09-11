var mongoose = require('mongoose');
mongoose.connect('mongodb://localhost/t4i');
//console.log(process.cwd());
var Student = require('../models/Student');
var student1 = {first_name: 'john', last_name: 'smith', id: 321};

Student.create( student1, function(err, student) {
  console.log('creating student');
  if (err) {
    console.error(err);
  } else {
    console.log(student);
  }
});

var QuestionSet = require('../models/QuestionSet');
var qs1 =
{
        "question_set_id": "city_2016_my_english",
        "author": "tni@teachforindia.org",
        "description": "question set description goes here",
        "city": "HYD",
        "year": 2015,
        "type": "questions set type",
        "subject": "question set subject",
        "level": 2,
        "grade": 4,
        "questions" : {
                "1": {
                        "q_text": "what is your name?",
                        "maxMarks": 2,
                        "unit": "question unit",
                        "topic": "question topic",
                        "domain": "writing",
                        "objective": "personal response"
                },
                "2": {
                        "q_text": "what is your name?",
                        "maxMarks": 2,
                        "unit": "question unit",
                        "topic": "question topic",
                        "domain": "writing",
                        "objective": "personal response"
                }
        },
        "questionList" : ["1","2"]
};

QuestionSet.create( qs1, function(err, qs) {
  console.log('creating questionset');
  if (err) {
    console.log(err);
  } else {
    console.log(qs);
  }
});
console.log('here');
