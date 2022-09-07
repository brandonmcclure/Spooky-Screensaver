ifeq ($(OS),Windows_NT)
	SHELL := pwsh.exe
else
	SHELL := pwsh
endif

.SHELLFLAGS := -NoProfile -Command

.PHONY: all test clean lint

all: lint

lint:
	docker run -v $${PWD}:/tmp/lint oxsecurity/megalinter:v6

test:
	echo 'Not Implemented'
clean:
	echo 'Not Implemented'