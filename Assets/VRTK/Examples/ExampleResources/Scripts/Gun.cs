namespace VRTK.Examples
{
    using UnityEngine;

    public class Gun : VRTK_InteractableObject
    {
        private GameObject bullet;
        private float bulletSpeed = 5000f;
        private float bulletLife = 1f;

        public Material[] decals;
        public Color[] colors;
        public GameObject front, decalPref;
        private int layerMask = ~(1 << 9);
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            FireBullet();
        }

        protected void Start()
        {
            bullet = transform.Find("Bullet").gameObject;
            bullet.SetActive(false);
        }
        private void Update()
        {
            Debug.DrawRay(front.transform.position, -front.transform.forward, Color.green);
        }
        private void FireBullet()
        {
            RaycastHit hitInfo;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Ray ray = new Ray(front.transform.position, front.transform.forward);

            if (Physics.Raycast(ray, out hitInfo, 100f, layerMask))
            {
                SpawnDecal(hitInfo);
            }
            GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
            bulletClone.SetActive(true);
            Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed);
            Destroy(bulletClone, bulletLife);


        }

        private void SpawnDecal(RaycastHit hitInfo)
        {
            if (hitInfo.transform.tag.Contains("Wall"))
            {
                var decal = Instantiate(decalPref);
                System.Random rand = new System.Random();
                decal.GetComponent<MeshRenderer>().material = decals[rand.Next(0, 20)];
                decal.transform.position = hitInfo.point;
                decal.transform.forward = hitInfo.normal * -1f;
            }
          
        }
    }
}