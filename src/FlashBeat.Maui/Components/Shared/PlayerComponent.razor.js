let currentAudio = {};
let currentUrl = {};

export async function SetupAudioFileStream(elementId, contentStreamReference) {
    // Clean up previous audio if exists
    if (currentAudio[elementId]) {
        currentAudio[elementId].pause();
        URL.revokeObjectURL(currentUrl);
    }

    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    currentUrl[elementId] = URL.createObjectURL(blob);

    currentAudio[elementId] = document.getElementById(`flash-beat-player-audio-${elementId}`);
    currentAudio[elementId].src = currentUrl[elementId];
    currentAudio[elementId].type = 'audio/mpeg';
    currentAudio[elementId].volume = 0.5;
    currentAudio[elementId].load();
}

export function PauseAudioFileStream(elementId) {
    if (currentAudio[elementId] && !currentAudio[elementId].paused) {
        currentAudio[elementId].pause();
    }
}

export function ResumeAudioFileStream(elementId) {
    if (currentAudio[elementId] && currentAudio[elementId].paused) {
        currentAudio[elementId].play();
    }
}

export function StopAudioFileStream(elementId) {
    if (currentAudio[elementId]) {
        currentAudio[elementId].pause();
        currentAudio[elementId].currentTime = 0;
    }
}

export function CleanupAudio(elementId) {
    if (currentAudio[elementId]) {
        currentAudio[elementId].pause();
        currentAudio[elementId].remove();
        URL.revokeObjectURL(currentUrl[elementId]);
        currentAudio[elementId] = null;
        currentUrl[elementId] = null;
    }
}