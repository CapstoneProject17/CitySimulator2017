var samplesPerSecond : int = 44100; // This is the standard sample rate for audio clips

function PlayCompleteSound(delay) {
	var audioSource = GetComponent(AudioSource);
	audioSource.Play(float.Parse(delay) * samplesPerSecond);
}