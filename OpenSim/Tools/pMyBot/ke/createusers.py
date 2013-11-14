#!/usr/bin/python
from sys import argv

if len(argv) != 5:
	print "Usage: %s <firstname> <lastname_prefix> <password> <no_of_bots>" % argv[0]
  	exit(-1)

firstName = argv[1]
lastNameStub = argv[2]
password = argv[3]
botCount = int(argv[4])

email = "zunlin1234@gmail.com"
idStub = "b0b0b0b0-0000-0000-0000-000000000"

for i in range(botCount):
	print "create user %s %s_%s %s %s %s%03i" % (firstName, lastNameStub, i, password, email, idStub, i)

