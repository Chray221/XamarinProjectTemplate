using System;

namespace XamProjectTemplate.DependencyServices
{
    public interface IBrightnessService
    {
        void SetBrightness(float brightness);
        void ResetBrightness();
    }
}
