let currentAudio = null;
let currentUrl = null;

export async function SetupAudioFileStream(contentStreamReference) {
    // Clean up previous audio if exists
    if (currentAudio) {
        currentAudio.pause();
        URL.revokeObjectURL(currentUrl);
    }

    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    currentUrl = URL.createObjectURL(blob);
    
    currentAudio = document.getElementById('flash-beat-player-audio');
    currentAudio.src = currentUrl;
    currentAudio.type = 'audio/mpeg';
    currentAudio.volume = 0.5;
    currentAudio.load();
}

export function PauseAudioFileStream() {
    if (currentAudio && !currentAudio.paused) {
        currentAudio.pause();
    }
}

export function ResumeAudioFileStream() {
    if (currentAudio && currentAudio.paused) {
        currentAudio.play();
    }
}

export function StopAudioFileStream() {
    if (currentAudio) {
        currentAudio.pause();
        currentAudio.currentTime = 0;
    }
}

export function CleanupAudio() {
    if (currentAudio) {
        currentAudio.pause();
        document.body.removeChild(currentAudio);
        URL.revokeObjectURL(currentUrl);
        currentAudio = null;
        currentUrl = null;
    }
}