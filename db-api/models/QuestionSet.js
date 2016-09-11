var mongoose = require('mongoose');

var fs = require('fs');
var jsonSchema = JSON.parse(fs.readFileSync('utils/dbschema/question_set.json', 'utf8'));
//var jsonSchema = require('question_set.json');
var converter = require('json-schema-converter');
var mongooseSchema = converter.to_mongoose_schema(jsonSchema);
var QuestionSetSchema = mongoose.Schema(mongooseSchema);

module.exports = mongoose.model('QuestionSet', QuestionSetSchema);
