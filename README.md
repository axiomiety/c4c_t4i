[![Build Status](https://travis-ci.org/axiomiety/c4c_t4i.svg?branch=master)](https://travis-ci.org/axiomiety/c4c_t4i)

# Code For Change - Teach For India

## Dependencies

`npm` is a must and `docker` is a nice to have (for running `mongodb`).

## TL;DR

  * Clone the repo: `git clone https://github.com/axiomiety/c4c_t4i.git`
  * Install the `node` dependencies: `$ npm install`
  * Start the `mongodb` docker container: `$ npm run mongostart`
  * Populate the db with some data: `$ npm run dbinit`
  * Start the express webserver (REST API): `$ npm run db-api`
  * Try it out with curl: `curl localhost:8080/students`

The last command should have the output below:

    $ curl localhost:8080/students
    [{"_id":"57c6d1f903def1cd65d3cd6d","first_name":"john","last_name":"smith","id":321,"__v":0,"last_updated":"2016-08-31T12:47:53.690Z"}]

## Tests

Use `npm run tests` or `mocha`.

## MongoDB

### With Docker

If you're using a Mac, there are some caveats as Docker needs to run inside a VM - meaning if you're trying to access the docker container *outside* of the VM (say by doing all your dev work *inside* a VM), you'll need to play around with port forwarding etc... @sharma-shweta has some more information.

### Importing sample objects

We don't want to write schemas by hand - really. Use something like `$ mongoimport --jsonArray -d t4i -c questionsets --file utils/dbobjects/question_set.json` to import the data directly into mongo.

Note that for `mongoose`, the collection name is usually the plural of the model name - so QuestionSet goes into the questionsets collection.

# Outstanding tasks

- [ ] use `nodemon` to automatically pick up changes to `db-api`
- [ ] use `grunt` instead of `npm` for all the wiring?
- [ ] save the mongodb data locally and mount through the container
- [ ] authentication much?
- [X] provide a way to generate models based on sample objects (did this for `QuestionSet` - should be easy to replicate)
