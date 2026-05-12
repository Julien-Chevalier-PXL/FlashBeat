namespace FlashBeat.ExternalServices.Delegates;

using FlashBeat.ExternalServices.Dtos.Responses;

public delegate Task<TrackDto?> GetTrackOfDelegate(long id, int index = 0, CancellationToken cancellationToken = default);
