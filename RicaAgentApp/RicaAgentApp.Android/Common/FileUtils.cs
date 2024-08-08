
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace RicaAgentApp.Droid.Common
{
    public class FileUtils
    {
        private static Android.Net.Uri filePathUri = null;


        public static String getPath(Context context, Android.Net.Uri uri)
        {
            filePathUri = uri;
            bool isKitKat = Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat;
            // DocumentProvider
            if (isKitKat && DocumentsContract.IsDocumentUri(context, uri))
            {
                // ExternalStorageProvider
                if (isExternalStorageDocument(uri))
                {
                    String docId = getDocumentId(DocumentsContract.GetDocumentId(uri));
                    String[] split = docId.Split(":");
                    String type = split[0];

                    if ("primary".Equals(type.ToLower()))
                    {
                        return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads) + "/" + split[1];
                    }
                    // TODO handle non-primary volumes
                }
                // DownloadsProvider
                else if (isDownloadsDocument(uri))
                {

                    String id = getDocumentId(DocumentsContract.GetDocumentId(uri));


                    Android.Net.Uri contentUri = ContentUris.WithAppendedId(
                            Android.Net.Uri.Parse("content://downloads/public_downloads"), Convert.ToInt64(id));


                    return getDataColumn(context, contentUri, null, null);
                }
                // MediaProvider
                else if (isMediaDocument(uri))
                {
                    String docId = getDocumentId(DocumentsContract.GetDocumentId(uri));
                    String[] split = docId.Split(":");
                    String type = split[0];

                    Android.Net.Uri contentUri = null;
                    if ("image".Equals(type))
                    {
                        contentUri = MediaStore.Images.Media.ExternalContentUri;
                    }
                    else if ("video".Equals(type))
                    {
                        contentUri = MediaStore.Video.Media.ExternalContentUri;
                    }
                    else if ("audio".Equals(type))
                    {
                        contentUri = MediaStore.Audio.Media.ExternalContentUri;
                    }

                    String selection = "_id=?";
                    String[] selectionArgs = new String[]{
                        split[1]
                };

                    return getDataColumn(context, contentUri, selection, selectionArgs);
                }
            }
            // MediaStore (and general)
            else if ("content".Equals(uri.Scheme.ToLower()))
            {
                return getDataColumn(context, uri, null, null);
            }
            // File
            else if ("file".Equals(uri.Scheme.ToLower()))
            {
                return uri.Path;
            }

            return null;
        }

        /**
         * Get the value of the data column for this Uri. This is useful for
         * MediaStore Uris, and other file-based.
         *
         * @param context       The context.
         * @param uri           The Uri to query.
         * @param selection     (Optional) Filter used in the query.
         * @param selectionArgs (Optional) Selection arguments used in the query.
         * @return The value of the _data column, which is typically a file path.
         */
        public static String getDataColumn(Context context, Android.Net.Uri uri, String selection,
                                           String[] selectionArgs)
        {

            ICursor cursor = null; 
            String column = "_data";
            String[] projection = {
                column
            };
            FileInputStream input = null;
            FileOutputStream output = null;

            try
            {
                cursor = context.ContentResolver.Query(uri, projection, selection, selectionArgs, null);
                if (cursor != null && cursor.MoveToFirst())
                {
                    int index = cursor.GetColumnIndexOrThrow(column);
                    return cursor.GetString(index);
                }
            }
            catch (Java.Lang.IllegalArgumentException e)
            {
                File file = new File(context.CacheDir, "tmp");
                String filePath = file.AbsolutePath;

                try
                {
                    ParcelFileDescriptor pfd = context.ContentResolver.OpenFileDescriptor(filePathUri, "r");
                    if (pfd == null)
                        return null;

                    FileDescriptor fd = pfd.FileDescriptor;
                    input = new FileInputStream(fd);
                    output = new FileOutputStream(filePath);
                    int read;
                    byte[] bytes = new byte[4096];
                    while ((read = input.Read(bytes)) != -1)
                    {
                        output.Write(bytes, 0, read);
                    }

                    input.Close();
                    output.Close();
                    return new File(filePath).AbsolutePath;
                }
                catch (IOException ignored)
                {
                }
            }
            finally
            {
                if (cursor != null)
                    cursor.Close();
            }
            return null;
        }

        /**
         * @param uri The Uri to check.
         * @return Whether the Uri authority is ExternalStorageProvider.
         */
        public static bool isExternalStorageDocument(Android.Net.Uri uri)
        {
            return "com.android.externalstorage.documents".Equals(uri.Authority);
        }

        /**
         * @param uri The Uri to check.
         * @return Whether the Uri authority is DownloadsProvider.
         */
        public static bool isDownloadsDocument(Android.Net.Uri uri)
        {
            return "com.android.providers.downloads.documents".Equals(uri.Authority);
        }

        /**
         * @param uri The Uri to check.
         * @return Whether the Uri authority is MediaProvider.
         */
        public static bool isMediaDocument(Android.Net.Uri uri)
        {
            return "com.android.providers.media.documents".Equals(uri.Authority);
        }


        private static String getDocumentId(String id)
        {
            if (!TextUtils.IsEmpty(id))
            {
                if (id.StartsWith("raw:"))
                {
                    return id.Replace("raw:", "");
                }

            }
            return id;
        }
    }
}