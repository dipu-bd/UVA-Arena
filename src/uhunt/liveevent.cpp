#include "liveevent.h"

using namespace uva;

LiveEvent LiveEvent::fromJsonObject(const QJsonObject &obj)
{
    LiveEvent liveEvent;
    

    liveEvent.LiveEventID = obj["id"].toVariant().toULongLong();
    liveEvent.Type = obj["type"].toString();

    liveEvent.UserSubmission = 
        UserSubmission::fromJsonObject(obj["msg"].toObject());

    return liveEvent;
}
