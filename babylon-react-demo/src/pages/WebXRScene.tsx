import { FC, useEffect, useRef } from "react";
import { 
  ArcRotateCamera,
  Color3,
  Engine,
  EnvironmentHelper,
  HemisphericLight,
  Mesh,
  MeshBuilder,
  Scene,
  StandardMaterial,
  Vector3,
  WebXRDefaultExperience
} from "@babylonjs/core";

// Required for EnvironmentHelper
import "@babylonjs/core/Materials/Textures/Loaders";
// Enable GLTF/GLB loader for loading controller models
import "@babylonjs/loaders/glTF";
// Required for controller model loading
import "@babylonjs/core/Materials/Node/Blocks";

const WebXRScene: FC = () => {
  const canvasRef = useRef<HTMLCanvasElement>(null);

  useEffect(() => {
    if (!canvasRef.current) return;

    // Create engine and scene
    const engine = new Engine(canvasRef.current, true);
    const scene = new Scene(engine);

    // Add a basic light
    new HemisphericLight("light1", new Vector3(0, 2, 0), scene);

    // Create a default environment
    const envHelper = new EnvironmentHelper(
      {
        skyboxSize: 30,
        groundColor: new Color3(0.5, 0.5, 0.5),
      },
      scene
    );

    // Add camera
    const camera = new ArcRotateCamera(
      "Camera",
      -(Math.PI / 4) * 3,
      Math.PI / 4,
      10,
      new Vector3(0, 0, 0),
      scene
    );
    camera.attachControl(true);

    // Add sphere
    const sphereD = 1.0;
    const sphere = MeshBuilder.CreateSphere(
      "xSphere",
      { segments: 16, diameter: sphereD },
      scene
    );
    sphere.position.x = 0;
    sphere.position.y = sphereD * 2;
    sphere.position.z = 0;

    const rMat = new StandardMaterial("matR", scene);
    rMat.diffuseColor = new Color3(1.0, 0, 0);
    sphere.material = rMat;

    // Setup WebXR
    const initXR = async () => {
      try {
        await WebXRDefaultExperience.CreateAsync(scene, {
          floorMeshes: [envHelper?.ground as Mesh],
          optionalFeatures: true,
        });
      } catch (error) {
        console.error("WebXR initialization failed:", error);
      }
    };
    
    initXR();

    // Start render loop
    engine.runRenderLoop(() => {
      scene.render();
    });

    // Handle window resize
    const handleResize = () => {
      engine.resize();
    };
    window.addEventListener('resize', handleResize);

    // Cleanup
    return () => {
      window.removeEventListener('resize', handleResize);
      engine.dispose();
    };
  }, []);

  return (
    <canvas 
      ref={canvasRef}
      style={{ 
        width: '100%', 
        height: '100vh',
        display: 'block',
        touchAction: 'none'
      }}
    />
  );
};

export default WebXRScene; 