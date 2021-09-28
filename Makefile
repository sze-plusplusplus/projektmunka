SHELL := /bin/bash

help:           	## Show this help.
	@echo Help:
	@echo -------------------------
	@fgrep -h "##" $(MAKEFILE_LIST) | fgrep -v fgrep | sed -e 's/\\$$//' | sed -e 's/##/-/'

up:			## Start docker dev env
	DUID=$(shell id -u) DGID=$(shell id -g) docker-compose up

.PHONY: publish
publish:		## Create a release docker image, exported to ./publish/
	@mkdir -p ./publish
	@docker-compose -f docker-compose.publish.yml build && docker save meethut > ./publish/meethut.tar

image:			## Start the release image
	@docker-compose -f docker-compose.image.yml up

gettoken:		## Create a new JWT token for livekit
	@docker run --rm -v ${PWD}/.livekit_config.yml:/config.yml livekit/livekit-server --config="/config.yml" create-join-token --room $(room) --identity $(user)

build-be:		## Build backend in release mode
	@pushd MeetHut.Backend/ && dotnet build; popd

build-fe:		## Build Frontend
	@pushd MeetHut.Backend/ClientApp && yarn build; popd

build:			## Build FE & BE
build: build-fe build-be 

test:			## Run BE and FE tests
	@pushd MeetHut.Backend/ && dotnet test; popd
	@pushd MeetHut.Backend/ClientApp && yarn test; popd
