# Journey to the Holocene

Educational VR museum experience developed for Senckenberg Naturmuseum Frankfurt and Meta Quest 3.

[Experience, trailer and project details](https://www.omidameri.com/project-holocene.html) · [Omid Ameri's portfolio](https://www.omidameri.com/)

## The experience

A collaborative university project combining an immersive environment, guided interactions, environmental storytelling and spatial audio. The Unity project lives in the P-3 Project subfolder.

## Explore the implementation

This public repository is a source-code showcase of a collaborative university project. It contains selected project scripts, their Unity metadata, the package manifest and the original editor version. It does not contain the complete playable project.

Project code is organized under:

- `P-3 Project/Assets/Scripts/`
- `P-3 Project/Assets/_Scripts/`


| Area | Entry point |
| --- | --- |
| Guided narrative and audio | [SoundManager.cs](P-3%20Project/Assets/_Scripts/Sounds%20Scripts/SoundManager.cs) |
| Animal trust interaction | [MastodonTrustMechanic.cs](P-3%20Project/Assets/_Scripts/Sounds%20Scripts/MastodonTrustMechanic.cs) |
| Potion interactions | [Potion.cs](P-3%20Project/Assets/_Scripts/PotionBrewing/Potion.cs) |
| Shaman state machine | [Shaman.cs](P-3%20Project/Assets/_Scripts/ShamanStateMachine/Shaman.cs) |
| Movement onboarding | [MovementTutorial.cs](P-3%20Project/Assets/_Scripts/tutoiral/MovementTutorial.cs) |
| Scene progression | [SceneSwapper.cs](P-3%20Project/Assets/_Scripts/SceneSwapper.cs) |

## Dependencies and running the project

The original project uses Unity **2022.3.48f1**. Review `Packages/manifest.json` inside the project folder for its package dependencies. To use these scripts, create an appropriate Unity project and restore the required packages. Scenes, prefabs, input bindings, art, audio and third-party plugins must be obtained and configured separately; cloning this showcase alone will not reproduce the game or experience.

Third-party Asset Store packages, vendor SDK source, course starter code, tutorial examples, models, textures, audio, compiled builds and generated editor files are intentionally excluded. Any plugins referenced by the scripts must be installed from their original publishers under the applicable licenses.

## Authorship and provenance

Developed collaboratively by the project team, including Omid Ameri. The code is presented as team work, not as an assertion that every file was written by one person. The complete project, contributor history, branches and tags are preserved in a separate private archive. This public showcase begins with a fresh snapshot so excluded files cannot be recovered from older commits.

No blanket open-source license is granted by this publication. Existing authorship and rights remain applicable; obtain permission from the relevant authors before reusing code.
