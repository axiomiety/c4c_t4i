var mongoose = require('mongoose');

var StudentSchema = new mongoose.Schema({
  first_name: String,
  last_name: String,
  id: Number,
  last_updated: {type: Date, default: Date.now}
});

module.exports = mongoose.model('Student', StudentSchema);
