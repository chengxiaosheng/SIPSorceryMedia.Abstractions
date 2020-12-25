﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SIPSorceryMedia.Abstractions
{
    public delegate void OnRawAudioSampleDelegate(AudioSamplingRatesEnum samplingRate, uint durationMilliseconds, short[] sample);
    public delegate void OnEncodedAudioSampleDelegate(uint durationRtpUnits, byte[] sample, AudioFormat encodedFormat);
    public delegate void OnRawVideoSampleDelegate(uint durationMilliseconds, VideoSample rawSample);
    public delegate void OnEncodedVideoSampleDelegate(uint durationRtpUnits, byte[] sample, VideoFormat encodedFormat);
    public delegate void OnDecodedVideoSampleDelegate(VideoSample decodedSample);
    public delegate void SourceErrorDelegate(string errorMessage);

    public enum AudioSamplingRatesEnum
    {
        Rate8KHz = 8000,
        Rate16KHz = 16000,
    }

    public enum VideoPixelFormatsEnum
    {
        Rgb = 0,        // 24 bits per pixel.
        Bgr = 1,        // 24 bits per pixel.
        Bgra = 2,       // 32 bits per pixel.
        I420 = 3,
        NV12 = 4,
    }

    /// <summary>
    /// A list of standard media formats that can be identified by an ID if
    /// there is no qualifying format attribute provided.
    /// </summary>
    /// <remarks>
    /// For definition of well known types see: https://tools.ietf.org/html/rfc3551#section-6.
    /// </remarks>
    public enum SDPWellKnownMediaFormatsEnum
    {
        PCMU = 0,       // Audio (8000/1).
        GSM = 3,        // Audio (8000/1).
        G723 = 4,       // Audio (8000/1).
        DVI4 = 5,       // Audio (8000/1).
        DVI4_16K = 6,   // Audio (16000/1).
        LPC = 7,        // Audio (8000/1).
        PCMA = 8,       // Audio (8000/1).
        G722 = 9,       // Audio (8000/1).
        L16_2 = 10,     // Audio (44100/2).
        L16 = 11,       // Audio (44100/1).
        QCELP = 12,     // Audio (8000/1).
        CN = 13,        // Audio (8000/1).
        MPA = 14,       // Audio (90000/*).
        G728 = 15,      // Audio (8000/1).
        DVI4_11K = 16,  // Audio (11025/1).
        DVI4_22K = 17,  // Audio (22050/1).
        G729 = 18,      // Audio (8000/1).

        CELB = 24,  // Video (90000).
        JPEG = 26,  // Video (90000).
        NV = 28,    // Video (90000).
        H261 = 31,  // Video (90000).
        MPV = 32,   // Video (90000).
        MP2T = 33,  // Audio/Video (90000).
        H263 = 34,  // Video (90000).
    }

    public static class AudioVideoWellKnown
    {
        public static Dictionary<SDPWellKnownMediaFormatsEnum, AudioFormat> WellKnownAudioFormats =
            new Dictionary<SDPWellKnownMediaFormatsEnum, AudioFormat> {
                { SDPWellKnownMediaFormatsEnum.PCMU,     new AudioFormat(AudioCodecsEnum.PCMU, 0, 8000, 1)},
                { SDPWellKnownMediaFormatsEnum.GSM,      new AudioFormat(AudioCodecsEnum.GSM,  3, 8000, 1)},
                { SDPWellKnownMediaFormatsEnum.G723,     new AudioFormat(AudioCodecsEnum.G723, 4, 8000, 1)},
                { SDPWellKnownMediaFormatsEnum.DVI4,     new AudioFormat(AudioCodecsEnum.DVI4, 5, 8000, 1)},
                { SDPWellKnownMediaFormatsEnum.DVI4_16K, new AudioFormat(AudioCodecsEnum.DVI4, 6, 16000, 1)},
                { SDPWellKnownMediaFormatsEnum.LPC,      new AudioFormat(AudioCodecsEnum.LPC,  7, 8000, 1)},
                { SDPWellKnownMediaFormatsEnum.PCMA,     new AudioFormat(AudioCodecsEnum.PCMA, 8, 8000, 1)},
                { SDPWellKnownMediaFormatsEnum.G722,     new AudioFormat(AudioCodecsEnum.G722, 9, 16000, 8000, 1, null)},
                { SDPWellKnownMediaFormatsEnum.L16_2,    new AudioFormat(AudioCodecsEnum.L16,  10, 44100, 2)},
                { SDPWellKnownMediaFormatsEnum.L16,      new AudioFormat(AudioCodecsEnum.L16,  11, 44100, 1)},
                { SDPWellKnownMediaFormatsEnum.QCELP,    new AudioFormat(AudioCodecsEnum.QCELP,12, 8000, 1)},
                { SDPWellKnownMediaFormatsEnum.CN,       new AudioFormat(AudioCodecsEnum.CN,   13, 8000, 1)},
                { SDPWellKnownMediaFormatsEnum.MPA,      new AudioFormat(AudioCodecsEnum.MPA,  14, 90000, 1)},
                { SDPWellKnownMediaFormatsEnum.G728,     new AudioFormat(AudioCodecsEnum.G728, 15, 8000, 1)},
                { SDPWellKnownMediaFormatsEnum.DVI4_11K, new AudioFormat(AudioCodecsEnum.DVI4, 16, 11025, 1)},
                { SDPWellKnownMediaFormatsEnum.DVI4_22K, new AudioFormat(AudioCodecsEnum.DVI4, 17, 22050, 1)},
                { SDPWellKnownMediaFormatsEnum.G729,     new AudioFormat(AudioCodecsEnum.G729, 18, 8000, 1)},
            };

        public static Dictionary<SDPWellKnownMediaFormatsEnum, VideoFormat> WellKnownVideoFormats =
           new Dictionary<SDPWellKnownMediaFormatsEnum, VideoFormat> {
                { SDPWellKnownMediaFormatsEnum.CELB,     new VideoFormat(VideoCodecsEnum.CELB, 24, 90000)},
                { SDPWellKnownMediaFormatsEnum.JPEG,     new VideoFormat(VideoCodecsEnum.JPEG, 26, 90000)},
                { SDPWellKnownMediaFormatsEnum.NV,       new VideoFormat(VideoCodecsEnum.NV,   28, 90000)},
                { SDPWellKnownMediaFormatsEnum.H261,     new VideoFormat(VideoCodecsEnum.H261, 31, 90000)},
                { SDPWellKnownMediaFormatsEnum.MPV,      new VideoFormat(VideoCodecsEnum.MPV,  32, 90000)},
                { SDPWellKnownMediaFormatsEnum.MP2T,     new VideoFormat(VideoCodecsEnum.MP2T, 33, 90000)},
                { SDPWellKnownMediaFormatsEnum.H263,     new VideoFormat(VideoCodecsEnum.H263, 34, 90000)}
           };
    }

    public enum AudioCodecsEnum
    {
        PCMU,
        GSM,
        G723,
        DVI4,
        LPC,
        PCMA,
        G722,
        L16,
        QCELP,
        CN,
        MPA,
        G728,
        G729,
        OPUS,

        PCM_S16LE,  // PCM signed 16-bit little-endian (equivalent to FFmpeg s16le). For use with Azure, not likely to be supported in VoIP/WebRTC.

        Unknown
    }

    public enum VideoCodecsEnum
    {
        CELB,
        JPEG,
        NV,
        H261,
        MPV,
        MP2T,
        H263,
        VP8,
        VP9,
        H264,
        H265,

        Unknown
    }

    public struct AudioFormat
    {
        public const int DYNAMIC_ID_MIN = 96;
        public const int DYNAMIC_ID_MAX = 127;
        public const int DEFAULT_CLOCK_RATE = 8000;
        public const int DEFAULT_CHANNEL_COUNT = 1;

        public static readonly AudioFormat Empty = new AudioFormat()
            { _isNonEmpty = false, ClockRate = DEFAULT_CLOCK_RATE, ChannelCount = DEFAULT_CHANNEL_COUNT };

        public AudioCodecsEnum Codec { get; set; }

        /// <summary>
        /// The format ID for the codec. If this is a well known codec it should be set to the
        /// value from the codec enum. If the codec is a dynamic it must be set between 96–127
        /// inclusive.
        /// </summary>
        public int FormatID { get; set; }

        /// <summary>
        /// The official name for the codec. This field is critical for dynamic codecs
        /// where it is used to match the codecs in the SDP offer/answer.
        /// </summary>
        public string FormatName { get; set; }

        /// <summary>
        /// The rate used to set RTP timestamps and to be set in the SDP format
        /// attribute for this format. It should almost always be the same as the
        /// <seealso cref="ClockRate"/>. An example of where it's not is G722 which
        /// uses a sample rate of 16KHz but an RTP rate of 8KHz for historical reasons.
        /// </summary>
        /// <example>
        /// In the SDP format attribute below the RTP clock rate is 48000.
        /// a=rtpmap:109 opus/48000/2
        /// </example>
        public int RtpClockRate { get; set; }

        /// <summary>
        /// The rate used by decoded samples for this audio format.
        /// </summary>
        public int ClockRate { get; set; }

        /// <summary>
        /// The number of channels for the audio format.
        /// </summary>
        /// <example>
        /// In the SDP format attribute below the channel count is 2.
        /// Note for single channel codecs the parameter is typically omitted from the
        /// SDP format attribute.
        /// a=rtpmap:109 opus/48000/2
        /// </example>
        public int ChannelCount { get; set; }

        /// <summary>
        /// This is the string that goes in the SDP "a=fmtp" parameter.
        /// This field should be set WITHOUT the "a=fmtp:" prefix.
        /// </summary>
        /// <example>
        /// In the case below this filed should be set as "emphasis=50-15".
        /// a=fmtp:97 emphasis=50-15
        /// </example>
        public string Parameters { get; set; }

        private bool _isNonEmpty;

        /// <summary>
        /// Creates a new audio format based on a well known SDP format.
        /// </summary>
        public AudioFormat(SDPWellKnownMediaFormatsEnum wellKnown) :
            this(AudioVideoWellKnown.WellKnownAudioFormats[wellKnown])
        { }

        /// <summary>
        /// Creates a new audio format based on a well known codec.
        /// </summary>
        public AudioFormat(
            AudioCodecsEnum codec,
            int formatID,
            int clockRate = DEFAULT_CLOCK_RATE,
            int channelCount = DEFAULT_CHANNEL_COUNT,
            string parameters = null) :
            this(codec, formatID, clockRate, clockRate, channelCount, parameters)
        { }

        /// <summary>
        /// Creates a new audio format based on a well known codec.
        /// </summary>
        public AudioFormat(
            AudioCodecsEnum codec,
            int formatID,
            int clockRate,
            int rtpClockRate,
            int channelCount,
            string parameters)
             : this(formatID, codec.ToString(), clockRate, rtpClockRate, channelCount, parameters)
        { }

        /// <summary>
        /// Creates a new audio format based on a dynamic codec (or an unsupported well known codec).
        /// </summary>
        public AudioFormat(
            int formatID,
            string formatName,
            int clockRate = DEFAULT_CLOCK_RATE,
            int channelCount = DEFAULT_CHANNEL_COUNT,
            string parameters = null) :
            this(formatID, formatName, clockRate, clockRate, channelCount, parameters)
        { }

        public AudioFormat(AudioFormat format)
            : this(format.FormatID, format.FormatName, format.ClockRate, format.RtpClockRate, format.ChannelCount, format.Parameters)
        { }

        /// <summary>
        /// Creates a new audio format based on a dynamic codec (or an unsupported well known codec).
        /// </summary>
        public AudioFormat(int formatID, string formatName, int clockRate, int rtpClockRate, int channelCount, string parameters)
        {
            if (formatID < 0)
            {
                // Note format ID's less than the dynamic start range are allowed as the codec list
                // does not currently support all well known codecs.
                throw new ApplicationException("The format ID for an AudioFormat must be greater than 0.");
            }
            else if (formatID > DYNAMIC_ID_MAX)
            {
                throw new ApplicationException($"The format ID for an AudioFormat exceeded the maximum allowed vale of {DYNAMIC_ID_MAX}.");
            }
            else if (string.IsNullOrWhiteSpace(formatName))
            {
                throw new ApplicationException($"The format name must be provided for an AudioFormat.");
            }
            else if(clockRate <= 0)
            {
                throw new ApplicationException($"The clock rate for an AudioFormat must be greater than 0.");
            }
            else if (rtpClockRate <= 0)
            {
                throw new ApplicationException($"The RTP clock rate for an AudioFormat must be greater than 0.");
            }
            else if (channelCount <= 0)
            {
                throw new ApplicationException($"The channel count for an AudioFormat must be greater than 0.");
            }

            FormatID = formatID;
            FormatName = formatName;
            ClockRate = clockRate;
            RtpClockRate = rtpClockRate;
            ChannelCount = channelCount;
            Parameters = parameters;
            _isNonEmpty = true;

            if (Enum.TryParse<AudioCodecsEnum>(FormatName, out var audioCodec))
            {
                Codec = audioCodec;
            }
            else
            {
                Codec = AudioCodecsEnum.Unknown;
            }
        }

        public bool IsEmpty() => !_isNonEmpty;
    }

    public struct VideoFormat
    {
        public const int DYNAMIC_ID_MIN = 96;
        public const int DYNAMIC_ID_MAX = 127;
        public const int DEFAULT_CLOCK_RATE = 90000;

        public static readonly VideoFormat Empty = new VideoFormat()
            { _isNonEmpty = false, ClockRate = DEFAULT_CLOCK_RATE };

        public VideoCodecsEnum Codec { get; set; }

        /// <summary>
        /// The format ID for the codec. If this is a well known codec it should be set to the
        /// value from the codec enum. If the codec is a dynamic it must be set between 96–127
        /// inclusive.
        /// </summary>
        public int FormatID { get; set; }

        /// <summary>
        /// The official name for the codec. This field is critical for dynamic codecs
        /// where it is used to match the codecs in the SDP offer/answer.
        /// </summary>
        public string FormatName { get; set; }

        /// <summary>
        /// The rate used by decoded samples for this video format.
        /// </summary>
        /// <remarks>
        /// Example, 90000 is the clock rate:
        /// a=rtpmap:102 H264/90000
        /// </remarks>
        public int ClockRate { get; set; }

        /// <summary>
        /// This is the "a=fmtp" format parameter that will be set in the SDP offer/answer.
        /// This field should be set WITHOUT the "a=fmtp:0" prefix.
        /// </summary>
        /// <remarks>
        /// Example:
        /// a=fmtp:102 level-asymmetry-allowed=1;packetization-mode=1;profile-level-id=42e01f"
        /// </remarks>
        public string Parameters { get; set; }

        private bool _isNonEmpty;

        /// <summary>
        /// Creates a new video format based on a well known SDP format.
        /// </summary>
        public VideoFormat(SDPWellKnownMediaFormatsEnum wellKnown) :
            this(AudioVideoWellKnown.WellKnownVideoFormats[wellKnown])
        { }

        /// <summary>
        /// Creates a new video format based on a well known codec.
        /// </summary>
        public VideoFormat(VideoCodecsEnum codec, int formatID, int clockRate = DEFAULT_CLOCK_RATE, string parameters = null)
            : this(formatID, codec.ToString(), clockRate, parameters)
        { }

        public VideoFormat(VideoFormat format)
            : this(format.FormatID, format.FormatName, format.ClockRate, format.Parameters)
        { }

        /// <summary>
        /// Creates a new video format based on a dynamic codec (or an unsupported well known codec).
        /// </summary>
        public VideoFormat(int formatID, string formatName, int clockRate = DEFAULT_CLOCK_RATE, string parameters = null)
        {
            if (formatID < 0)
            {
                // Note format ID's less than the dynamic start range are allowed as the codec list
                // does not currently support all well known codecs.
                throw new ApplicationException("The format ID for an VideoFormat must be greater than 0.");
            }
            else if (formatID > DYNAMIC_ID_MAX)
            {
                throw new ApplicationException($"The format ID for an VideoFormat exceeded the maximum allowed vale of {DYNAMIC_ID_MAX}.");
            }
            else if (string.IsNullOrWhiteSpace(formatName))
            {
                throw new ApplicationException($"The format name must be provided for a VideoFormat.");
            }
            else if (clockRate <= 0)
            {
                throw new ApplicationException($"The clock rate for a VideoFormat must be greater than 0.");
            }

            FormatID = formatID;
            FormatName = formatName;
            ClockRate = clockRate;
            Parameters = parameters;
            _isNonEmpty = true;

            if (Enum.TryParse<VideoCodecsEnum>(FormatName, out var videoCodec))
            {
                Codec = videoCodec;
            }
            else
            {
                Codec = VideoCodecsEnum.Unknown;
            }
        }

        public bool IsEmpty() => !_isNonEmpty;
    }

    public class MediaEndPointInterfaces
    {
        public IAudioSource AudioSource { get; set; }
        public IAudioSink AudioSink { get; set; }
        public IVideoSource VideoSource { get; set; }
        public IVideoSink VideoSink { get; set; }
    }

    public interface IAudioEncoder
    {
        /// <summary>
        /// Checks whether the encoder supports a particular audio format.
        /// </summary>
        /// <param name="format">The audio format to check support for.</param>
        /// <returns>True if the encode and decode operations are supported for the audio format.</returns>
        bool IsSupported(AudioFormat format);

        /// <summary>
        /// Encodes 16bit signed PCM samples.
        /// </summary>
        /// <param name="pcm">An array of 16 bit signed audio samples.</param>
        /// <param name="format">The audio format to encode the PCM sample to.</param>
        /// <returns>A byte array containing the encoded sample.</returns>
        byte[] EncodeAudio(short[] pcm, AudioFormat format);

        /// <summary>
        /// Decodes to 16bit signed PCM samples.
        /// </summary>
        /// <param name="encodedSample">The byte array containing the encoded sample.</param>
        /// <param name="format">The audio format of the encoded sample.</param>
        /// <returns>An array containing the 16 bit signed PCM samples.</returns>
        short[] DecodeAudio(byte[] encodedSample, AudioFormat format);
    }

    public struct VideoSample
    {
        public uint Width;
        public uint Height;
        public uint Stride;
        public byte[] Sample;
        public VideoPixelFormatsEnum PixelFormat;
    }

    public interface IVideoEncoder : IDisposable
    {
        /// <summary>
        /// Checks whether a video format is supported by this encoder.
        /// </summary>
        /// <param name="format">The video format to check.</param>
        /// <returns>True if the format is supported or false if not.</returns>
        bool IsSupported(VideoFormat format);

        /// <summary>
        /// Encodes a video sample.
        /// </summary>
        /// <param name="sample">The raw sample to encode.</param>
        /// <param name="format">The encoded video format to encode the sample with.</param>
        /// <param name="forceKeyFrame">If set to true the encoder will be explicitly requested to
        /// encode the sample as a key frame.</param>
        /// <returns>A buffer holding the encoded video sample.</returns>
        byte[] EncodeVideo(VideoSample sample, VideoFormat format, bool forceKeyFrame);

        /// <summary>
        /// Decodes an encoded video sample.
        /// </summary>
        /// <param name="encodedSample">The encoded sample to decode.</param>
        /// <param name="format">The format of the encoded sample.</param>
        /// <param name="desiredOutputFormat">This parameter should be set to the desired output pixel format. The 
        /// encoder may not support the format. The pixel format of each decoded sample must be checked.</param>
        /// <returns>A list of decoded video samples. Can be empty. A decode call can also return multiple samples.</returns>
        IEnumerable<VideoSample> DecodeVideo(byte[] encodedSample, VideoFormat format, VideoPixelFormatsEnum desiredOutputFormat);
    }

    public interface IAudioSource
    {
        event OnEncodedAudioSampleDelegate OnEncodedAudioSample;

        event OnRawAudioSampleDelegate OnRawAudioSample;

        event SourceErrorDelegate OnAudioSourceError;

        Task PauseAudio();

        Task ResumeAudio();

        Task StartAudio();

        Task CloseAudio();

        List<AudioFormat> GetEncoderFormats();

        void SetEncoderFormat(AudioFormat audioFormat);

        void RestrictFormats(Func<AudioFormat, bool> filter);

        void ExternalAudioSourceRawSample(AudioSamplingRatesEnum samplingRate, uint durationMilliseconds, short[] sample);

        bool HasEncodedAudioSubscribers();

        bool IsAudioSourcePaused();
    }

    public interface IAudioSink
    {
        event SourceErrorDelegate OnAudioSinkError;

        List<AudioFormat> GetAudioSinkFormats();

        void GotAudioRtp(IPEndPoint remoteEndPoint, uint ssrc, uint seqnum, uint timestamp, int payloadID, bool marker, byte[] payload);

        void RestrictFormats(Func<AudioFormat, bool> filter);

        Task PauseAudioSink();

        Task ResumeAudioSink();

        Task StartAudioSink();

        Task CloseAudioSink();
    }

    public interface IVideoSource
    {
        event OnEncodedVideoSampleDelegate OnEncodedVideoSample;

        event OnRawVideoSampleDelegate OnRawVideoSample;

        event SourceErrorDelegate OnVideoSourceError;

        Task PauseVideo();

        Task ResumeVideo();

        Task StartVideo();

        Task CloseVideo();

        List<VideoFormat> GetEncoderFormats();

        void SetEncoderFormat(VideoFormat videoFormat);

        void RestrictFormats(Func<VideoFormat, bool> filter);

        void ForceKeyFrame();

        bool HasEncodedVideoSubscribers();

        bool IsVideoSourcePaused();
    }

    public interface IVideoSink
    {
        /// <summary>
        /// This event will be fired by the sink after it decodes a video frame from the RTP stream.
        /// </summary>
        event OnDecodedVideoSampleDelegate OnDecodedVideoSample;

        /// <summary>
        /// This method gets called on the sink when a full video frame has been depacketised from RTP 
        /// packets. If the underlying RTP logic does not know how to depacketise the video stream
        /// the <see cref="GotVideoRtp"/> should be used instead.
        /// </summary>
        /// <param name="remoteEndPoint">The remote end point the encoded frame packet(s) were received from.</param>
        /// <param name="timestamp">The RTP timestamp for the encoded frame.</param>
        /// <param name="encodedFrame">The buffer containing the encoded video frame.</param>
        /// <param name="encodedFormat">The format of the encoded video frame. Typically the format 
        /// is negotiated by exchanging SDP offer/answers and then matched against the RTP header
        /// payload ID.</param>
        void GotVideoFrame(IPEndPoint remoteEndPoint, uint timestamp, byte[] encodedFrame, VideoFormat encodedFormat);

        /// <summary>
        /// This method should only be used where the paketisation format of the video stream is not supported.
        /// It will be up to the consumer of the event to reconstruct the encoded video frames from the individual
        /// RTP packets.
        /// </summary>
        /// <param name="remoteEndPoint">The remote end point the RTP packets were received from.</param>
        /// <param name="ssrc">The synchronisation source for the RTP packet.</param>
        /// <param name="seqnum">The sequence number for the RTP packet.</param>
        /// <param name="timestamp">The timestamp in RTP units for the RTP packet.</param>
        /// <param name="payloadID">The payload ID for the RTP packet.</param>
        /// <param name="marker">True if the marker bit was set on the RTP packet.</param>
        /// <param name="encodedPayload">The RTP packet payload.</param>
        void GotVideoRtp(IPEndPoint remoteEndPoint, uint ssrc, uint seqnum, uint timestamp, int payloadID, bool marker, byte[] rtpPayload);

        List<VideoFormat> GetDecoderFormats();

        void RestrictFormats(Func<VideoFormat, bool> filter);

        /// <summary>
        /// Pauses the decoding of a video stream. Note that pausing the decoding of a video stream means a new key frame will
        /// need to be requested from the encoder when the stream is resumed.  Other than key frames each encoded video 
        /// frame depends on a set of previous decoded frames. If the decoder misses a frame then typically all subsequent frames
        /// up until the next key frame will not be able to be decoded.
        /// </summary>
        Task PauseVideoSink();
        
        Task ResumeVideoSink();
        
        Task StartVideoSink();
        
        Task CloseVideoSink();
    }
}