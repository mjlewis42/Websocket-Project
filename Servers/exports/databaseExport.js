var mysql =require('mysql');

var pool  = mysql.createPool({
    connectionLimit :100,
    host            :'[REDACTED]',
    user            :'[REDACTED]',
    password        :'[REDACTED]',
    database        :'wizardclient'
  });

  module.exports = {pool};