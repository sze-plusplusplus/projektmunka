SHELL := /bin/bash

ISMAC = 0
UNAME_S := $(shell uname -s)
ifeq ($(UNAME_S),Darwin)
	ISMAC = 1
endif
DOCKER_PARAMS = DUID=$(shell id -u) DGID=$(shell id -g) PLATFORM="linux/amd64" ISMAC=$(ISMAC)
IMAGE_VERSION = v0.1

help:           	## Show this help.
	@echo Help:
	@echo -------------------------
	@fgrep -h "##" $(MAKEFILE_LIST) | fgrep -v fgrep | sed -e 's/\\$$//' | sed -e 's/##/-/'

build-compose:		## (Re-)build dev docker images
	@$(DOCKER_PARAMS) docker-compose build

up:			## Start docker dev env
	@$(DOCKER_PARAMS) docker-compose up

.PHONY: publish
publish:		## Create a release docker image, pushes to github container registry (ghcr login/write permission required)
	@docker buildx create --use --name=meethut --node=meethut
	@$(DOCKER_PARAMS) docker buildx build --platform linux/amd64,linux/arm64/v8 --output "type=image,push=true" --tag "ghcr.io/sze-plusplusplus/meethut:$(IMAGE_VERSION)" -f Dockerfile.publish .

image:			## Start the release image
	@$(DOCKER_PARAMS) docker-compose -f docker-compose.image.yml up

gettoken:		## Create a new JWT token for livekit
	@$(DOCKER_PARAMS) docker run --rm -v ${PWD}/.livekit_config.yml:/config.yml livekit/livekit-server --config="/config.yml" create-join-token --room $(room) --identity $(user)

build-be:		## Build backend in release mode
	@pushd MeetHut.Backend/ && dotnet build; popd

build-fe:		## Build Frontend
	@pushd MeetHut.Backend/ClientApp && yarn build; popd

build:			## Build FE & BE
build: build-fe build-be 

test:			## Run BE and FE tests
	@pushd MeetHut.Backend/ && dotnet test; popd
	@pushd MeetHut.Backend/ClientApp && yarn test; popd

add-migration:	## Add backend EF migration -> make add-migration name=Init
	@pushd MeetHut.Backend/ && DOTNET_USEDESIGNTIMECONNECTION=True dotnet ef migrations add $(name) --project ../MeetHut.DataAccess/ --no-build; popd