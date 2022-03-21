module PROJECT {
  /**
   * Babylon Script Component
   * @class TestUi
   */
  export class TestUi extends BABYLON.ScriptComponent {
    // Example: private helloWorld:string = "Hello World";

    protected awake(): void {
      /* Init component function */
    }

    protected start(): void {
      new AdvancedTextureUi().create();
    }

    protected ready(): void {
      /* Execute when ready function */
    }

    protected update(): void {
      /* Update render loop function */
    }

    protected late(): void {
      /* Late update render loop function */
    }

    protected after(): void {
      /* After update render loop function */
    }

    protected fixed(): void {
      /* Fixed update physics step function */
    }

    protected destroy(): void {
      /* Destroy component function */
    }
  }
}
