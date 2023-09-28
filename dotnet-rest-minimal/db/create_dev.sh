#!/usr/bin/env bash

set -o errexit
set -o nounset
set -o pipefail
if [[ "${TRACE-0}" == "1" ]]; then
    set -o xtrace
fi

print_help() {
	echo 'Help!!'
}

if [[ "${1-}" =~ ^-*h(elp)?$ ]]; then
		print_help
    exit
fi

cd "$(dirname "$0")"

main() {
    echo 'Pulling and creating new DEV postgres docker instance'
		docker run --name postgres-neon-dev -e POSTGRES_USER=neon -e POSTGRES_PASSWORD=neon -e POSTGRES_DB=neon -d -p 5432:5432 postgres 
}

main "$@"
