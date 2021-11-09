#!/bin/bash

## Update Postgres database (update existed database to actual state)
dotnet ef database update -v -p ../../Otus.Project.Orm -s ../../Otus.Project.CrudApi