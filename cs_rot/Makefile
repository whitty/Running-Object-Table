ifdef WINE

ifndef MONO_DIR
MONO_DIR=c:/Program Files/Mono-2.6.7
endif
MONO_LIB=$(MONO_DIR)/lib/mono/2.0

MSCS="$(WINE)" "$(MONO_DIR)/bin/mono.exe" "$(MONO_LIB)/gmcs.exe"

else

MSCS=gmcs

endif

.PHONY: clean

SUPPRESS_WARNINGS =
CS_FLAGS += $(foreach _,$(SUPPRESS_WARNINGS),-nowarn:$(_))
CS_FLAGS += -unsafe

OBJECTS = rot.dll

all : $(OBJECTS)

rot.dll : rot.cs

%.dll : %.cs
	$(MSCS) $(CS_FLAGS) -target:library $^

ifneq "$(strip $(wildcard $(OBJECTS)))" ""
clean ::
	-rm -f $(wildcard $(OBJECTS))
endif

