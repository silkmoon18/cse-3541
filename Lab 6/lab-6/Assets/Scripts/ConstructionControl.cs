using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConstructionControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;

    [SerializeField] private GameObject[] constructions;
    [SerializeField] private GameObject constructionUI;
    [SerializeField] private Text woodText;
    [SerializeField] private Text warnText;

    [SerializeField] private float distance;

    private int[] prices = { 1, 2, 2, 3 };

    private bool isConstructing = false;

    private int numberOfWoods = 0;

    private GameObject selected;

    private InputAction constructAction;
    private InputAction attackAction;

    public void Initialize(InputAction buildAction, InputAction attackAction)
    {
        constructionUI.SetActive(false);

        this.constructAction = buildAction;
        buildAction.Enable();

        this.attackAction = attackAction;
        attackAction.Enable();

        Color color = warnText.color;
        color.a = 0f;
        warnText.color = color;
    }

    private void Update()
    {
        if (constructAction.WasPerformedThisFrame())
        {
            ToggleConstructPage(!constructionUI.activeSelf);
        }

        woodText.text = "You have " + numberOfWoods + " woods.";

        if (isConstructing)
        {
            ConstructSelected();
        }
    }

    private void ToggleConstructPage(bool isOn)
    {
        constructionUI.SetActive(isOn);
        if (constructionUI.activeSelf)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            player.GetComponent<PlayerControl>().allowAttack = false;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<PlayerControl>().allowAttack = true;
        }
        
    }
    public void SelectConstruction(int index)
    {
        if (numberOfWoods >= prices[index])
        {
            numberOfWoods -= prices[index];

            ToggleConstructPage(false);

            selected = Instantiate(constructions[index]);

            isConstructing = true;
        }
        else
        {
            StartCoroutine(PromptWarnText());
        }
    }

    private void ConstructSelected()
    {
        Vector3 direction = playerCamera.transform.forward;

        float d = distance + selected.GetComponent<Collider>().bounds.size.z;

        Vector3 position = player.transform.position + Vector3.up * 5 + direction * d;

        selected.transform.position = new Vector3(position.x, selected.transform.position.y, position.z);

        if (attackAction.WasPressedThisFrame())
        {
            isConstructing = false;

            player.GetComponent<PlayerControl>().allowAttack = true;
        }
    }
    public void AddWood(int num)
    {
        numberOfWoods += num;
    }

    private IEnumerator PromptWarnText()
    {
        Color color = warnText.color;
        color.a = 1f;
        warnText.color = color;

        yield return new WaitForSecondsRealtime(2f);

        float fadingTime = 2f;
        float timeSince = 0f;
        float delta = 0.1f;
        while(timeSince < fadingTime)
        {
            yield return new WaitForSecondsRealtime(delta);

            color = warnText.color;
            color.a -= delta / fadingTime;
            warnText.color = color;
            timeSince += delta;
        }
    }
}
