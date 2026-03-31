#!/bin/sh
printf '\033c\033]0;%s\a' binary space partitioning
base_path="$(dirname "$(realpath "$0")")"
"$base_path/5worldgen.x86_64" "$@"
