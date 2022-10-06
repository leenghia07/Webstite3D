function Storage() {

	const indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB;

	if ( indexedDB === undefined ) {

		console.warn( 'Storage: IndexedDB not available.' );
		return { init: function () {}, get: function () {}, set: function () {}, clear: function () {} };

	}

	const name = 'threejs-editor';
	const version = 1;

	let database;

	return {
		init: function ( callback ) {

			const request = indexedDB.open( name, version );
			request.onupgradeneeded = function ( event ) {

				const db = event.target.result;
				// db.createObjectStore( 'states' );
				if ( db.objectStoreNames.contains( 'states' ) === false ) {

					db.createObjectStore( 'states' );
					
				}

			};

			request.onsuccess = function ( event ) {

				database = event.target.result;

				callback();

			};

			request.onerror = function ( event ) {

				console.error( 'IndexedDB', event );

			};


		},
/*
		init: function ( ObjectName,callback) {

			const request = indexedDB.open( name, version );
			request.onupgradeneeded = function ( event ) {

				const db = event.target.result;

	
				if ( db.objectStoreNames.contains(ObjectName) === false ) {

					db.createObjectStore(ObjectName);

				}

			};

			request.onsuccess = function ( event ) {

				database = event.target.result;

				callback();

			};

			request.onerror = function ( event ) {

				console.error( 'IndexedDB', event );

			};


		},*/
		getAll:function(callback)
		{
			const transaction = database.transaction( [ 'states'  ], 'readwrite' );
			const objectStore = transaction.objectStore('states' );
			const request = objectStore.getAllKeys();
			request.onsuccess = function ( event ) {

				callback( event.target.result );

			};

		},
		getObject: function ( Uid ,callback) {

			const transaction = database.transaction( [ 'states' ], 'readwrite' );
			const objectStore = transaction.objectStore( 'states' );
			const request = objectStore.get(Uid);
			request.onsuccess = function ( event ) {

				callback( event.target.result );

			};

		},

		setObject: function ( data ,Uid) {

			const start = performance.now();

			const 	transaction = database.transaction( [ 'states' ], 'readwrite' );
			const objectStore = transaction.objectStore( 'states' );
			const request = objectStore.put( data, Uid );
			request.onsuccess = function () {

				console.log( '[' + /\d\d\:\d\d\:\d\d/.exec( new Date() )[ 0 ] + ']', 'Saved state to IndexedDB. ' + ( performance.now() - start ).toFixed( 2 ) + 'ms' );

			};

		},

		get: function ( callback ) {

			const transaction = database.transaction( [ 'states' ], 'readwrite' );
			const objectStore = transaction.objectStore( 'states' );
			const request = objectStore.get( 0 );
			request.onsuccess = function ( event ) {

				callback( event.target.result );

			};

		},

		set: function ( data ) {

			const start = performance.now();
			const id = "create"
			const 	transaction = database.transaction( [ 'states' ], 'readwrite' );
			const objectStore = transaction.objectStore( 'states' );
			const request = objectStore.put( data, id );
			request.onsuccess = function () {

				console.log( '[' + /\d\d\:\d\d\:\d\d/.exec( new Date() )[ 0 ] + ']', 'Saved state to IndexedDB. ' + ( performance.now() - start ).toFixed( 2 ) + 'ms' );

			};

		},
		remove: function (key) {

			if ( database === undefined ) return;

			const transaction = database.transaction( [ 'states' ], 'readwrite' );
			const objectStore = transaction.objectStore( 'states' );
			const request = objectStore.delete(key);
			request.onsuccess = function () {

				console.log( '[' + /\d\d\:\d\d\:\d\d/.exec( new Date() )[ 0 ] + ']', 'Cleared IndexedDB.' );

			};
			request.onerror = (err)=> {
				console.error(`Error to delete student: ${err}`)
			}
		},
		clear: function () {

			if ( database === undefined ) return;

			const transaction = database.transaction( [ 'states' ], 'readwrite' );
			const objectStore = transaction.objectStore( 'states' );
			const request = objectStore.clear();
			request.onsuccess = function () {

				console.log( '[' + /\d\d\:\d\d\:\d\d/.exec( new Date() )[ 0 ] + ']', 'Cleared IndexedDB.' );

			};

		}

	};

}

export { Storage };
