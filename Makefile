#General vars
CONFIG?=Debug
ARGS:=/p:Configuration="${CONFIG}" $(ARGS)

all:
	echo "Building FigmaSharp..."
	msbuild MotionSharp.sln $(ARGS)

clean:
	find . -type d -name bin -exec rm -rf {} \;
	find . -type d -name obj -exec rm -rf {} \;
	find . -type d -name packages -exec rm -rf {} \;

check-dependencies:
	#if test "x$(CONFIG)" = "xDebug" || test "x$(CONFIG)" = "xRelease"; then \
	#	./bot-provisioning/system_dependencies.sh || exit 1; \
	#fi

nuget-download:
	# nuget restoring
	if [ ! -f src/.nuget/nuget.exe ]; then \
		mkdir -p src/.nuget ; \
	    echo "nuget.exe not found! downloading latest version" ; \
	    curl -O https://dist.nuget.org/win-x86-commandline/latest/nuget.exe ; \
	    mv nuget.exe src/.nuget/ ; \
	fi

submodules: nuget-download
	git submodule update --init --recursive
	echo "Restoring FigmaSharp..."
	msbuild MotionSharp.sln /t:Restore

sdk: nuget-download
	mono src/.nuget/nuget.exe pack MotionSharp.nuspec


.PHONY: all clean pack install submodules sdk nuget-download check-dependencies
