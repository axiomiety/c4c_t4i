# Code For Change - Teach For India

## Dependencies

To

## TL;DR

  * Clone the repo: `git clone https://github.com/axiomiety/c4c_t4i.git`
  * Install the `node` dependencies: `$ npm install`
  * Start the `mongodb` docker container: `$ npm run mongostart`
  * Populate the db with some data: `$ npm run dbinit`
  * Start the express webserver (REST API): `$ cd db-api;PORT=8080 npm start`
  * Try it out with curl: ``

## Tests

Use `npm run tests` or `mocha`.

# Outstanding tasks

- [ ] Use `nodemon` to automatically pick up changes to `db-api`
- [ ] use `grunt` instead of `npm` for all the wiring?
- [ ] save the mongodb data locally and mount through the container

