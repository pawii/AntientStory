using UnityEngine;

public class Arrow : MonoBehaviour 
{
    private GameObject lightPrefab;
    private GameObject light;

    #region Unity lifecycle

    private void Awake()
	{
        lightPrefab = Resources.Load<GameObject>("ArrowLight");

		ArchTorso.Shoot += this.Shoot;
        Light.AddLight += Arrow_OnAddLight;
        Finish.FinishLevel += Arrow_OnFinishLevel;
        PlayerManager.ReloadGame += Arrow_OnFinishLevel;

        if (Managers.Player.HasLigth)
        {
            Arrow_OnAddLight();
        }
	}


	private void OnDestroy()
	{
		ArchTorso.Shoot -= this.Shoot;
        Light.AddLight -= Arrow_OnAddLight;
        Finish.FinishLevel -= Arrow_OnFinishLevel;
        PlayerManager.ReloadGame -= Arrow_OnFinishLevel;
    }

	#endregion


	private void Shoot(float charge, int damage)
	{
		Vector3 force = !Player.instance.isLookingToTheRight ? -transform.right : transform.right;

		force *= charge;

		if (Managers.Player.HasLigth)
			BulletFactory.CreateArrowWithLight(transform, damage, force, tag);
		else
			BulletFactory.CreateArrow(transform, damage, force, tag);
	}


    private void Arrow_OnAddLight()
    {
        if (light == null)
        {
            light = Instantiate(lightPrefab);
            light.transform.parent = transform;
            light.transform.localScale = Vector3.one;
            light.transform.localEulerAngles = Vector3.zero;
            light.transform.localPosition = Vector3.zero;

            Managers.Player.HasLigth = true;
        }
    }


    private void Arrow_OnFinishLevel()
    {
        if (Managers.Player.HasLigth)
        {
            Destroy(light);
            Managers.Player.HasLigth = false;
        }
    }
}
